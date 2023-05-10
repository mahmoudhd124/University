import SubjectFileTypes from "../Subject/SubjectFileTypes";

export interface SubjectMaterialModel {
    id: number;
    name: string;
    storedName: string;
    subjectId: number;
    date: string;
    type:SubjectFileTypes
}