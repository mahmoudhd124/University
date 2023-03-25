import { useParams } from "react-router-dom"
import { useGetDoctorQuery } from "../../App/Api/DoctorApi"
import { Alert } from 'react-bootstrap'
import useGetAppError from "../../Hookes/useGetAppError"
import './DoctorPage.css'

const DoctorPage = () => {
    const { id } = useParams()
    const { data, isError, error, isSuccess, isFetching } = useGetDoctorQuery(id!)
    return (
        <div className="container">
            {isError && <Alert variant="danger">{useGetAppError(error)?.message}</Alert>}
            <div className="d-flex flex-column gap-2">
                <div className='row justify-content-center'>
                    <span className="col-3">First Name: </span>
                    <span className="fw-bold col-5">{data?.firstname}</span>
                </div>

                <div className='row justify-content-center'>
                    <span className="col-3">Last Name: </span>
                    <span className="fw-bold col-5">{data?.lastname}</span>
                </div>

                <div className='row justify-content-center'>
                    <span className="col-3">Username: </span>
                    <span className="fw-bold col-5">{data?.username}</span>
                </div>

                <div className='row justify-content-center'>
                    <span className="col-3">Email: </span>
                    <span className="fw-bold col-5">{data?.email}</span>
                </div>

                <div className='row justify-content-center'>
                    <span className="col-3">Phone Number: </span>
                    <span className="fw-bold col-5">{data?.phoneNumber}</span>
                </div>

                <div className='row justify-content-center'>
                    <span className="col-3">National Number: </span>
                    <span className="fw-bold col-5">{data?.nationalNumber}</span>
                </div>
            </div>

            <div className="row justify-content-center justify-content-sm-around gap-3">
                {data?.subjects?.length ?? 0 > 0 ? data?.subjects.map(s => <div key={s.id}
                    className="col-auto border border-3 rounded rounded-3 p-3 subject">
                    {s.department}{s.code}{'   '}{s.name}
                </div>) : (
                    <>
                        <h3 className='text-center mt-5 text-danger'>This Doctor Is Not Assigne To Any Subject!!</h3>
                    </>
                )}
            </div>
        </div>
    )
}

export default DoctorPage