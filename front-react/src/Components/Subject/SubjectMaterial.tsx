import React, {useEffect, useRef, useState} from 'react';
import TimeAgo from "../Global/TimeAgo";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faDownload, faTrash} from "@fortawesome/free-solid-svg-icons";
import {BASE_URL} from "../../App/Api/BaseApi";
import {SubjectMaterialModel} from "../../Models/SubjectMaterial/SubjectMaterialModel";
import {
    SubjectMaterialApi,
    useGetSubjectMaterialQuery,
    useLazyGetSubjectMaterialQuery
} from "../../App/Api/SubjectMaterialApi";

interface Props {
    material: SubjectMaterialModel,
    isOwner: boolean,
    removeHandler: (e: React.MouseEvent) => void
}

const SubjectMaterial = ({material: m, isOwner, removeHandler}: Props) => {
    // const [get, {data, isFetching, error, isSuccess}] = useLazyGetSubjectMaterialQuery()
    // const link = useRef() as React.MutableRefObject<HTMLAnchorElement>

    // useEffect(() => {
    //     if (isSuccess && data ) {
    //         console.log(data )
    //         link.current.href = URL.createObjectURL(data)
    //         link.current.click()
    //     }
    // }, [data,isSuccess])
    //
    // const download = (e: React.MouseEvent) => {
    //     e.preventDefault()
    //     get(m.storedName)
    // }


    return (
        <div
            className="col-12 col-md-5 col-lg-3 bg-white border border-3 rounded rounded-3 d-flex flex-column
            align-items-center mx-1 my-1 card">
            <h5 className={'my-1 card-title'}>{m.name}</h5>
            <p className={'mt-auto'}><TimeAgo timestamp={m.date}/></p>
            <div className="row my-1 card-footer w-100 justify-content-center">
                {isOwner ? <>
                        <div className="col-6 text-center">
                            <FontAwesomeIcon className={'btn btn-outline-danger w-75'}
                                             onClick={removeHandler}
                                             icon={faTrash}/>
                        </div>
                        <div className="col-6 text-center">
                            {/*onClick={download}>*/}
                            <a href={SubjectMaterialApi + m.storedName + '/' + m.name}>
                                <FontAwesomeIcon className={'btn btn-outline-primary w-75'}
                                                 icon={faDownload}/>
                            </a>
                        </div>
                    </> :
                    <div className="col-12 btn btn-outline-primary w-100">
                        {/*onClick={download}>*/}
                        <a href={SubjectMaterialApi + m.storedName + '/' + m.name}>
                            <FontAwesomeIcon className={'btn btn-outline-primary w-75'}
                                             icon={faDownload}/>
                        </a>

                    </div>
                }
            </div>
        </div>
    );
};

export default SubjectMaterial;