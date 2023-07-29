import {SubjectFileModel} from '../SubjectMaterial/SubjectFileModel'

export interface SubjectModel {
    id: number;
    department: string;
    code: number;
    hours: number;
    name: string;
    hasADoctor: boolean;
    doctorId: string;
    doctorUsername: string;
    doctorProfilePhoto:string,
    isOwner: boolean;
    files: SubjectFileModel[];
    totalNumberOfFilesRequired:number
}