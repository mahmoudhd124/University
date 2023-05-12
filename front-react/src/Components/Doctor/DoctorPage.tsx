import {useLocation, useNavigate, useParams} from "react-router-dom"
import {useGetDoctorQuery} from "../../App/Api/DoctorApi"
import {Alert} from 'react-bootstrap'
import useGetAppError from "../../Hookes/useGetAppError"
import './DoctorPage.css'
import DoctorField from "./DoctorField";
import SubjectFileTypes from "../../Models/Subject/SubjectFileTypes";
import useAppSelector from "../../Hookes/useAppSelector";

const DoctorPage = () => {
    const {id} = useParams()
    const {data, isError, error, isFetching} = useGetDoctorQuery(id ?? '')
    const loc = useLocation()
    const isAdmin = useAppSelector(s => s.auth.roles)?.some(r => r.toLowerCase() == 'admin')
    const navigator = useNavigate()

    if (isFetching) {
        const s: JSX.Element[] = []
        for (let i = 0; i < 4; i++) {
            s.push(
                <div key={i}
                     className={`col-11 col-sm-8 col-md-5 col-lg-3 d-flex flex-column align-items-center
                     border border-3 rounded-3 p-3 text-center`}>
                    <h3 className={'field__looding'}></h3>
                    <h3 className={'mt-1 field__looding'}></h3>
                    <p className={'field__looding'}></p>
                </div>
            )
        }
        return <div className="container">
            <div className="d-flex flex-column">
                <button className={'w-50 btn btn-outline-dark border-0 field__looding mx-auto my-3'}></button>
                <div className={'subject__looding'}>
                    <h3 className={'field__looding'}></h3>
                    <h3 className={'field__looding'}></h3>
                    <h3 className={'field__looding'}></h3>
                    <h3 className={'field__looding'}></h3>
                    <h3 className={'field__looding'}></h3>
                    <h3 className={'field__looding'}></h3>
                </div>
                <div className="row justify-content-center justify-content-sm-around gap-3">
                    {s}
                </div>
            </div>
        </div>
    }

    const per = (data?.subjects
        .map(s => s.numberOfFilesTypes)
        .filter(n => n == (Object.keys(SubjectFileTypes).length / 2))
        .length ?? 1) / (data?.subjects.length ?? 1) * 100

    return (
        <div className="container">
            {isError && <Alert variant="danger" className={'text-center'}>{useGetAppError(error)?.message}</Alert>}
            {data?.isOwner && <div className="d-flex flex-column align-items-center my-2">
                <div className="btn btn-outline-dark w-50"
                     onClick={e => navigator('/doctor/edit/' + data.id, {state: {from: loc}})}
                > Edit
                </div>
            </div>}

            {isAdmin && <div className="row justify-content-center my-2 mb-5">
                <div className="col-12 col-md-8 col-lg-3">
                    <button className={'btn btn-danger w-100'}
                            onClick={e => navigator(`/message/send/${data?.id}/${data?.username}`)}
                    >Send Alarm</button>
                </div>
            </div>}

            <div className="d-flex flex-column gap-2">
                <DoctorField title={'First Name:'} data={data?.firstname!}/>
                <DoctorField title={'Last Name:'} data={data?.lastname!}/>
                <DoctorField title={'User Name:'} data={data?.username!}/>
                <DoctorField title={'Email:'} data={data?.email!}/>
                <DoctorField title={'Phone Number:'} data={data?.phoneNumber!}/>
                <DoctorField title={'National Number:'} data={data?.nationalNumber!}/>
            </div>

            <div className="text-primary">Has Done</div>
            <div className="progress">
                <div className="progress-bar" style={{width: `${per}%`}}>{per}%</div>
            </div>

            <div className="row justify-content-center justify-content-sm-around gap-3">
                {data?.subjects?.length ?? 0 > 0 ? data?.subjects.map(s =>
                    <div key={s.id}
                         className={'col-11 col-sm-8 col-md-5 col-lg-3 d-flex flex-column align-items-center border text-center border-3 rounded-3 subject'}
                         onClick={e => navigator('/subject/' + s.code, {state: {from: loc}})}>
                        <h3>{s.name}</h3>
                        <h3 className={'mt-1'}>{s.department}</h3>
                        <p>{s.code}</p>
                        <h5 className={'mt-1'}><b>{s.numberOfFilesTypes}</b> File Type Uploaded</h5>
                    </div>
                ) : (
                    <>
                        <h3 className='text-center mt-5 text-danger'>No Subjects Assigned!!</h3>
                        <p className='text-center text-danger'>
                            if you think that is wrong please contact the admin</p>
                    </>
                )}
            </div>
        </div>
    )
}

export default DoctorPage