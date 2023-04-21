import {SubjectMaterialModel} from "../../Models/SubjectMaterial/SubjectMaterialModel";
import TimeAgo from "../Global/TimeAgo";
import {BASE_URL} from "../../App/Api/BaseApi";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faDownload, faTrash} from "@fortawesome/free-solid-svg-icons";
import React, {useRef, useState} from "react";
import {useAddSubjectMaterialMutation, useDeleteSubjectMaterialMutation} from "../../App/Api/SubjectMaterialApi";
import {useNavigate} from "react-router-dom";
import useAppDispatch from "../../Hookes/useAppDispatch";
import useAppSelector from "../../Hookes/useAppSelector";

const SubjectMaterials = ({
                              materials,
                              isOwner,
                              id,
                              code
                          }: { materials: SubjectMaterialModel[], isOwner: boolean, id: number, code: number }) => {
    const dispatch = useAppDispatch()
    const token = useAppSelector(s => s.auth.token)
    const navigator = useNavigate()
    const add = useAddSubjectMaterialMutation(token!, dispatch)
    const file = useRef() as React.MutableRefObject<HTMLInputElement>
    const [idToRemove, setIdToRemove] = useState<number | null>(null)
    const [remove, removeResult] = useDeleteSubjectMaterialMutation()

    const addMaterialHandler = async (e: React.ChangeEvent<HTMLInputElement>) => {
        e.preventDefault()
        const f = file.current.files![0]
        if (f == undefined)
            return
        const data = new FormData()
        data.append('subjectId', id.toString())
        data.append('file', f)
        await add(data)
        navigator('/subject/' + code)
    }

    const removeHandlerOne = (id: number) => {
        return (e: React.MouseEvent<SVGSVGElement>) => {
            e.preventDefault()
            setIdToRemove(id)
        }
    }

    const removeHandlerTwo = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        remove({id: idToRemove!, subjectId: id})
        setIdToRemove(null)
    }

    const returnBack = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        setIdToRemove(null)
    }

    return (
        <div style={{backgroundColor: '#eee'}}>
            <h3 className={'text-center'}>Materials</h3>
            {idToRemove && <div
                className='border border-3 rounded rounded-3 mx-auto p-3 bg-white' style={{width: '90%'}}>
                <h3 className='text-center'>Are You Sure You Want To
                    Delete {materials?.filter(d => d.id == idToRemove)[0].name}</h3>
                <div className="row justify-content-center gap-4">
                    <div className="col-4">
                        <button onClick={removeHandlerTwo} className='btn btn-outline-danger'>Remove Any Way</button>
                    </div>

                    <div className="col-4">
                        <button onClick={returnBack} className='btn btn-outline-primary'>Return Back</button>
                    </div>
                </div>
            </div>}
            {isOwner &&
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

            <div className="container">
                <div className="row justify-content-around">
                    {materials.map(m =>
                        <div key={m.id}
                             className="col-12 col-md-5 col-lg-3 bg-white border border-3 rounded rounded-3 d-flex flex-column align-items-center mx-1 my-1 card">
                            <h5 className={'my-1 card-title'}>{m.name}</h5>
                            <p className={'mt-auto'}><TimeAgo timestamp={m.date}/></p>
                            <div className="row my-1 card-footer w-100 justify-content-center">
                                {isOwner ? <>
                                        <div className="col-6 text-center">
                                            <FontAwesomeIcon className={'btn btn-outline-danger w-75'}
                                                             onClick={removeHandlerOne(m.id)}
                                                             icon={faTrash}/>
                                        </div>
                                        <div className="col-6 text-center">
                                            <a href={BASE_URL.slice(0, -4) + 'SubjectMaterials/' + m.storedName}>
                                                <FontAwesomeIcon className={'btn btn-outline-primary w-75'}
                                                                 icon={faDownload}/>
                                            </a>
                                        </div>
                                    </> :
                                    <div className="col-12 btn btn-outline-primary w-100">
                                        <a href={BASE_URL.slice(0, -4) + 'SubjectMaterials/' + m.storedName}>
                                            <FontAwesomeIcon className={'btn btn-outline-primary w-75'}
                                                             icon={faDownload}/>
                                        </a>
                                    </div>
                                }
                            </div>
                        </div>)}

                </div>
            </div>
        </div>
    );
};

export default SubjectMaterials;