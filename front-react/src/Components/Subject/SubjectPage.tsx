import React, {useEffect, useRef, useState} from 'react';
import {
    useDeleteAssignedDoctorMutation,
    useGetSubjectByCodeQuery
} from "../../App/Api/SubjectApi";
import {useNavigate, useParams} from "react-router-dom";
import useGetAppError from "../../Hookes/useGetAppError";
import './SubjectPage.css'
import useAppSelector from "../../Hookes/useAppSelector";
import {useAssignDoctorToSubjectMutation, useGetDoctorPageQuery} from "../../App/Api/DoctorApi";
import SubjectMaterials from "./SubjectMaterials";

const SubjectPage = () => {
    const {code} = useParams()
    const p = useRef() as React.MutableRefObject<HTMLDivElement>
    const navigator = useNavigate()
    const {data: subject, isError, error, isFetching} = useGetSubjectByCodeQuery(Number(code))
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
        if (p.current == undefined)
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



    //todo fix the add operation it is not working

    if (isError || subject == undefined)
        return <h2>{useGetAppError(error)?.message}</h2>

    return (
        <div className="container" ref={p}>
            {isAdmin && <div className={'row justify-content-center'}>
                <button className={'col-8 col-md-6 btn btn-outline-dark my-3'}
                        onClick={e => navigator('/subject/edit/' + subject?.code!)}>Edit
                </button>
            </div>}


            <div className="card">
                <div className="card-header">
                    <h2 className={'text-center'}>{subject.name}</h2>
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
                    <hr/>
                    <SubjectMaterials materials={subject?.materials}
                                      isOwner={subject.isOwner}
                                      id={subject?.id!}
                                      code={subject?.code!}/>
                </div>
            </div>
        </div>
    );
};

export default SubjectPage;