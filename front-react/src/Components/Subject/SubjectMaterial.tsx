import React from 'react';
import TimeAgo from "../Global/TimeAgo";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faDownload, faTrash} from "@fortawesome/free-solid-svg-icons";
import {SubjectMaterialModel} from "../../Models/SubjectMaterial/SubjectMaterialModel";
import {SubjectMaterialApi,} from "../../App/Api/SubjectMaterialApi";
import useAxiosApi from "../../Hookes/useAxiosApi";
import useAppSelector from "../../Hookes/useAppSelector";

interface Props {
    material: SubjectMaterialModel,
    isOwner: boolean,
    removeHandler: (e: React.MouseEvent) => void,
    api: ReturnType<typeof useAxiosApi>, // Receive the api instance
    token: string | null, // Receive the token

}

const SubjectMaterial = ({material: m, isOwner, removeHandler, api, token}: Props) => {
    // const api = useAxiosApi()
    // const token = useAppSelector(s => s.auth.token)

    const handleDownload = () => {
        api<Blob>(SubjectMaterialApi + `${m.storedName}`)
            .then((response) => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', m.name);
                document.body.appendChild(link);
                link.click();
            })
            .catch((error) => {
                console.error('Error downloading file:', error);
            });
    }


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
                        <div className="col-6 text-center" onClick={handleDownload}>
                            <FontAwesomeIcon className={'btn btn-outline-primary w-75'} icon={faDownload}/>
                        </div>
                    </> :
                    <div className="col-12 btn btn-outline-primary w-100" onClick={handleDownload}>
                        <FontAwesomeIcon className={'btn btn-outline-primary w-75'} icon={faDownload}/>
                    </div>
                }
            </div>
        </div>
    );
};

export default SubjectMaterial;