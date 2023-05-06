import React, {useEffect, useRef, useState} from 'react';
import TimeAgo from "../Global/TimeAgo";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faDownload, faTrash} from "@fortawesome/free-solid-svg-icons";
import {BASE_URL} from "../../App/Api/BaseApi";
import {SubjectMaterialModel} from "../../Models/SubjectMaterial/SubjectMaterialModel";
import {useGetSubjectMaterialQuery, useLazyGetSubjectMaterialQuery} from "../../App/Api/SubjectMaterialApi";

interface Props {
    material: SubjectMaterialModel,
    isOwner: boolean,
    removeHandler: (e: React.MouseEvent) => void
}

const SubjectMaterial = ({material: m, isOwner, removeHandler}: Props) => {
    const [get, {data, isFetching, error, isSuccess}] = useLazyGetSubjectMaterialQuery()
    const link = useRef() as React.MutableRefObject<HTMLAnchorElement>

    // useEffect(() => {
    //     if (isSuccess) {
    //         console.log(data)
    //         link.current.href = URL.createObjectURL(data)
    //         link.current.click()
    //     }
    // }, [isSuccess])


    // if (data && isSuccess && error == undefined && data instanceof Blob) {
    //     console.log('here')
    //     const downloadUrl = URL.createObjectURL(data);
    //     const link = document.createElement('a');
    //     link.href = downloadUrl;
    //     link.setAttribute('download', m.storedName);
    //     document.body.appendChild(link);
    //     link.click();
    //     document.body.removeChild(link);
    //     URL.revokeObjectURL(downloadUrl);
    // }

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
                            <a href={BASE_URL.slice(0, -4) + 'SubjectMaterials/' + m.storedName}>
                                <FontAwesomeIcon className={'btn btn-outline-primary w-75'}
                                                 icon={faDownload}/>
                            </a>
                        </div>
                    </> :
                    <div className="col-12 btn btn-outline-primary w-100">
                        {/*onClick={download}>*/}
                        <a href={BASE_URL.slice(0, -4) + 'SubjectMaterials/' + m.storedName}>
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