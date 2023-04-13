import React, {useEffect, useRef, useState} from 'react';
import {
    useDeleteAssignedDoctorMutation,
    useGetSubjectByCodeQuery
} from "../../App/Api/SubjectApi";
import {useNavigate, useParams} from "react-router-dom";
import useGetAppError from "../../Hookes/useGetAppError";
import './SubjectPage.css'
import TimeAgo from "../Global/TimeAgo";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faDownload, faTrash} from '@fortawesome/free-solid-svg-icons'
import {useAddSubjectMaterialMutation, useDeleteSubjectMaterialMutation} from "../../App/Api/SubjectMaterialApi";
import useAppSelector from "../../Hookes/useAppSelector";
import {useAssignDoctorToSubjectMutation, useGetDoctorPageQuery} from "../../App/Api/DoctorApi";
import {BASE_URL, baseApi} from "../../App/Api/BaseApi";
import useAppDispatch from "../../Hookes/useAppDispatch";

const SubjectPage = () => {
    const {code} = useParams()
    const p = useRef() as React.MutableRefObject<HTMLDivElement>
    const dispatch = useAppDispatch()
    const navigator = useNavigate()
    const token = useAppSelector(s => s.auth.token)
    const {data: subject, isError, error, isFetching} = useGetSubjectByCodeQuery(Number(code))
    const [remove, removeResult] = useDeleteSubjectMaterialMutation()
    const [add, addResult] = useAddSubjectMaterialMutation()
    const file = useRef() as React.MutableRefObject<HTMLInputElement>
    const [idToRemove, setIdToRemove] = useState<number | null>(null)
    const isAdmin = useAppSelector(s => s.auth.roles)?.some(r => r.toLowerCase() == 'admin')
    const [deleteDoctor, deleteDoctorResult] = useDeleteAssignedDoctorMutation()
    const [assignDoctor, assignDoctorResult] = useAssignDoctorToSubjectMutation()
    const [doctorUsername, setDoctorUsername] = useState('')
    const doctorListResult = isAdmin ?
        useGetDoctorPageQuery({
            pageIndex: 0,
            pageSize: 10,
            usernamePrefix: doctorUsername
        }) : null

    useEffect(() => {
        if(p.current == undefined)
            return

        if (isFetching) {
            p.current.style.opacity = '.5'
            p.current.onmousemove = e => e.preventDefault()
        } else {
            p.current.style.opacity = '1'
            p.current.onmousemove = null
        }
    }, [isFetching])

    const deleteDoctorHandler = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        deleteDoctor(subject?.id!)
    }

    const assignDoctorHandler = (did: string) => {
        return (e: React.MouseEvent<HTMLButtonElement>) => {
            e.preventDefault()
            setDoctorUsername('')
            assignDoctor({did, sid: subject?.id!})
        }
    }


    const removeHandlerOne = (id: number) => {
        return (e: React.MouseEvent<SVGSVGElement>) => {
            e.preventDefault()
            setIdToRemove(id)
        }
    }

    const removeHandlerTwo = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        remove({id: idToRemove!, subjectId: subject?.id!})
        setIdToRemove(null)
    }

    const returnBack = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        setIdToRemove(null)
    }

    //todo fix the add operation it is not working
    const addMaterialHandler = async (e: React.ChangeEvent<HTMLInputElement>) => {
        e.preventDefault()
        const f = file.current.files![0]
        if (f == undefined)
            return
        const data = new FormData()
        data.append('subjectId', subject?.id.toString()!)
        data.append('file', f)
        //todo handle it with better way, may be with rtk query
        const response = await fetch(BASE_URL + 'subjectmaterial', {
            method: 'POST',
            body: data,
            headers: {
                'authorization': `Bearer ${token}`
            }
        });
        dispatch(baseApi.util.invalidateTags([{type: 'subject', id: subject?.id!}]))
        navigator('/subject/' + subject?.code!)
    }

    if (isError || subject == undefined)
        return <h2>{useGetAppError(error)?.message}</h2>

    return (
        <div className="container" ref={p}>
            {isAdmin && <div className={'row justify-content-center'}>
                <button className={'col-8 col-md-6 btn btn-outline-dark my-3'} onClick={e => navigator('/subject/edit/'+subject?.code!)}>Edit</button>
            </div>}
            {idToRemove && <div
                className='border border-3 roudned rounded-3 mx-auto p-3'>
                <h3 className='text-center'>Are You Sure You Want To
                    Delete {subject.materials?.filter(d => d.id == idToRemove)[0].name}</h3>
                <div className="row justify-content-center gap-4">
                    <div className="col-4">
                        <button onClick={removeHandlerTwo} className='btn btn-outline-danger'>Remove Any Way</button>
                    </div>

                    <div className="col-4">
                        <button onClick={returnBack} className='btn btn-outline-primary'>Return Back</button>
                    </div>
                </div>
            </div>}

            <div className="card">
                <div className="card-header">
                    <h2>{subject.name}</h2>
                </div>
                <div className="card-body">
                    <div className="row">
                        <div className="col-md-4">
                            <p>
                                <strong>Code:</strong> {subject.code}
                            </p>
                        </div>
                        <div className="col-md-4">
                            <p>
                                <strong>Department:</strong> {subject.department}
                            </p>
                        </div>
                        <div className="col-md-4">
                            <p>
                                <strong>Hours:</strong> {subject.hours}
                            </p>
                        </div>
                    </div>

                    {subject.hasADoctor ? <div className="row">
                        <div className="col-md-6">
                            <p>
                                <strong>Doctor:</strong> {subject.doctorUsername}
                            </p>
                        </div>
                        <div className="col-md-6">
                            <p>
                                <strong>Doctor ID:</strong> {subject.doctorId}
                            </p>
                        </div>
                        {isAdmin && <div className={'col-md-6'}>
                            <button className={'btn btn-outline-danger'}
                                    onClick={deleteDoctorHandler}
                            >Remove Doctor
                            </button>
                        </div>}
                    </div> : (<div className={'text-center text-danger'}>
                        <h3>This Subject Is Not Assigned To Any Doctor Yet!</h3>
                        <p>if you think that is wrong, please contact the admin</p>
                        <h4 className="my-3 text-black">Assign</h4>
                        <div className={'row justify-content-center'}>
                            <div className={'col-md-6'}>
                                <input type="text"
                                       placeholder={'Doctor Username'}
                                       className={'form-control'}
                                       onChange={e => setDoctorUsername(e.currentTarget.value)}
                                />
                                {doctorUsername.trim().length > 0 && <div className="list-group">
                                    {doctorListResult?.data?.map(d => <button
                                        className={'list-group-item list-group-item-action'}
                                        key={d.id}
                                        onClick={assignDoctorHandler(d.id)}
                                    >
                                        {d.username}
                                    </button>)}
                                </div>}
                            </div>
                        </div>
                    </div>)}

                    {subject.isOwner &&
                        <div className="row">
                            <div className="col-md-12 text-center mt-3">
                                <input type={'file'}
                                       ref={file}
                                       style={{display: 'none'}}
                                       onChange={addMaterialHandler}
                                />
                                <button className="btn btn-primary"
                                        onClick={e => file.current.click()}>
                                    Add Material
                                </button>
                            </div>
                        </div>
                    }
                    <hr/>
                    <h3>Materials:</h3>
                    <table className="table table-striped table-hover text-center">
                        <thead>
                        <tr>
                            <th scope={'col'}>#</th>
                            <th scope={'col'}>Name</th>
                            <th scope={'col'}>Date</th>
                            <th scope={'col'}></th>
                            {subject.isOwner && <th scope={'col'}></th>}
                        </tr>
                        </thead>
                        <tbody>
                        {subject.materials.map((material, i) => (
                            <tr key={material.id}>
                                <th scope={'row'}>{i + 1}</th>
                                <td>{material.name}</td>
                                <td><TimeAgo timestamp={material.date}/></td>
                                <td>
                                    <a href={BASE_URL.slice(0, -4) + 'SubjectMaterials/' + material.storedName}>
                                        <FontAwesomeIcon icon={faDownload} style={{cursor: 'pointer'}}/>

                                    </a>
                                </td>
                                {subject.isOwner &&
                                    <td className="text-right">
                                        <FontAwesomeIcon icon={faTrash}
                                                         className="text-danger"
                                                         style={{cursor: 'pointer'}}
                                                         onClick={removeHandlerOne(material.id)}/></td>
                                }
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default SubjectPage;