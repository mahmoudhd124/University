import {useLocation, useNavigate, useParams} from "react-router-dom"
import {useGetDoctorQuery} from "../../App/Api/DoctorApi"
import {Alert} from 'react-bootstrap'
import useGetAppError from "../../Hookes/useGetAppError"
import './DoctorPage.css'

const DoctorPage = () => {
    const {id} = useParams()
    const {data, isError, error, isFetching} = useGetDoctorQuery(id ?? '')
    const loc = useLocation()
    const navigator = useNavigate()

    if (isFetching)
        return <h2>Loading...</h2>

    return (
        <div className="container">
            {isError && <Alert variant="danger" className={'text-center'}>{useGetAppError(error)?.message}</Alert>}
            {data?.isOwner && <div className="d-flex flex-column align-items-center my-2">
                <div className="btn btn-outline-dark w-50"
                     onClick={e => navigator('/doctor/edit/' + data.id, {state: {from: loc}})}
                > Edit
                </div>
            </div>}
            <div className="d-flex flex-column gap-2">
                <div className='row justify-content-between justify-content-sm-center'>
                    <span className="col-12 col-sm-3">First Name: </span>
                    <span className="fw-bold col-12 col-sm-5">{data?.firstname}</span>
                </div>

                <div className='row justify-content-between justify-content-sm-center'>
                    <span className="col-12 col-sm-3">Last Name: </span>
                    <span className="fw-bold col-12 col-sm-5">{data?.lastname}</span>
                </div>

                <div className='row justify-content-between justify-content-sm-center'>
                    <span className="col-12 col-sm-3">Username: </span>
                    <span className="fw-bold col-12 col-sm-5">{data?.username}</span>
                </div>

                <div className='row justify-content-between justify-content-sm-center'>
                    <span className="col-12 col-sm-3">Email: </span>
                    <span className="fw-bold col-12 col-sm-5">{data?.email}</span>
                </div>

                <div className='row justify-content-between justify-content-sm-center'>
                    <span className="col-12 col-sm-3">Phone Number: </span>
                    <span className="fw-bold col-12 col-sm-5">{data?.phoneNumber}</span>
                </div>

                <div className='row justify-content-between justify-content-sm-center'>
                    <span className="col-12 col-sm-3">National Number: </span>
                    <span className="fw-bold col-12 col-sm-5">{data?.nationalNumber}</span>
                </div>
            </div>

            <div className="row justify-content-center justify-content-sm-around gap-3">
                {data?.subjects?.length ?? 0 > 0 ? data?.subjects.map(s => <div key={s.id}
                                                                                className="col-auto border border-3 rounded rounded-3 p-3 subject">
                    {s.department}{s.code}{'   '}{s.name}
                </div>) : (
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