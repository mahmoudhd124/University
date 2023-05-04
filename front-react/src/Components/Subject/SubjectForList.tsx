import React, {useState} from 'react';
import './SubjectForList.css'
import {SubjectForPageModel} from "../../Models/Subject/SubjectForPageModel";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faTrash} from "@fortawesome/free-solid-svg-icons";

interface Props {
    subject: SubjectForPageModel,
    onClickHandler: (e: React.MouseEvent<HTMLDivElement>) => void,
    onDelete: (e: React.MouseEvent) => void
}

const SubjectForList = ({subject: s, onClickHandler, onDelete}: Props) => {
    return (
        <div
            className={'col-11 col-sm-8 col-md-5 col-lg-3 d-flex flex-column align-items-center border text-center border-3 rounded-3 subject'}
            onClick={onClickHandler}
        >
                <h3>{s.name}</h3>
                <h3 className={'mt-1'}>{s.department}</h3>
                <p>{s.code}</p>
            <div className={'remove-subject'}
                 onClick={onDelete}
            >
                <FontAwesomeIcon icon={faTrash} className={'remove-subject-icon'}/>
            </div>
        </div>
    );
};

export default SubjectForList;