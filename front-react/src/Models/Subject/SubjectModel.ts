import {SubjectMaterialModel} from '../SubjectMaterial/SubjectMaterialModel'

export interface SubjectModel {
    id: number;
    department: string;
    code: number;
    hours: number;
    name: string;
    hasADoctor: boolean;
    doctorId: string;
    doctorUsername: string;
    isOwner: boolean;
    files: SubjectMaterialModel[];
    totalNumberOfFilesRequired:number
}