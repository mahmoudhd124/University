import React, {useRef, useState} from 'react'
import {Link, useLocation, useParams} from 'react-router-dom'
import {useDeleteDoctorMutation, useGetDoctorPageQuery} from '../../App/Api/DoctorApi'
import {Alert} from 'react-bootstrap'
import useGetAppError from '../../Hookes/useGetAppError'
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'
import {faTrash, faX, faCheck} from '@fortawesome/free-solid-svg-icons'
import Pagination from "../Pagination";
import SubjectFileTypes from "../../Models/Subject/SubjectFileTypes";

const PAGE_SIZE = 10
const DoctorsList = () => {
    const {pageIndex} = useParams()
    const [page, setPage] = useState(Number(pageIndex ?? '0'))
    const [usernamePrefix, setUsernamePrefix] = useState('')
    const loc = useLocation()
    const name = useRef() as React.MutableRefObject<HTMLInputElement>
    const [idToRevmoe, setIdToRemove] = useState<string | null>(null)
    const twoStepRemoveScreen = useRef() as React.MutableRefObject<HTMLDivElement>
    const [filterHasSubject, setFilterHasSubject] = useState<boolean | null>(null)


    const {data, isError, error} =
        useGetDoctorPageQuery({pageIndex: page, pageSize: PAGE_SIZE, usernamePrefix, hasSubject: filterHasSubject})

    const [remove, removeResult] = useDeleteDoctorMutation()


    const removeHandlerOne = (id: string) => {
        return (e: React.MouseEvent<SVGSVGElement>) => {
            e.preventDefault()
            setIdToRemove(id)
        }
    }

    const removeHandlerTwo = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        remove(idToRevmoe!)
        setIdToRemove(null)
    }

    const returnBack = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        setIdToRemove(null)
    }
    const selectHasSubjectOrNot = (className?: string) => {
        return <div className={"btn-group " + className ?? ''}>
            <button type="button" className="btn btn-outline-primary dropdown-toggle" data-bs-toggle="dropdown"
                    aria-expanded="false">
                {filterHasSubject == null ? 'All' : filterHasSubject ? 'Has Subjects' : 'Has No Subjects'}
            </button>
            <ul className="dropdown-menu w-75">
                <li onClick={e => setFilterHasSubject(null)}>
                    <div className="dropdown-item">All</div>
                </li>
                <li onClick={e => setFilterHasSubject(true)}>
                    <div className="dropdown-item">Has Subject</div>
                </li>
                <li onClick={e => setFilterHasSubject(false)}>
                    <div className="dropdown-item">Has No Subject</div>
                </li>
            </ul>
        </div>
    }


    return (
        <div className='container'>
            {isError && <Alert variant='danger'>{useGetAppError(error)?.message}</Alert>}

            {idToRevmoe && <div ref={twoStepRemoveScreen}
                                className='border border-3 roudned rounded-3 mx-auto p-3'>
                <h3 className='text-center'>Are You Sure You Want To
                    Delete {data?.filter(d => d.id == idToRevmoe)[0].username}</h3>
                <div className="row justify-content-center gap-4">
                    <div className="col-4">
                        <button onClick={removeHandlerTwo} className='btn btn-outline-danger'>Remove Any Way</button>
                    </div>

                    <div className="col-4">
                        <button onClick={returnBack} className='btn btn-outline-primary'>Return Back</button>
                    </div>
                </div>
            </div>}

            <div className='row justify-content-center'>
                <div className='col-8 col-md-5'>
                    <Link className='btn btn-outline-dark w-100 my-3' to={'/doctor/add'} state={{from: loc}}>Add
                        Doctor</Link>
                </div>
            </div>
            <h3>Doctors!</h3>

            <div className="input-group flex-nowrap my-3">
                <span className="input-group-text" id="addon-wrapping">@</span>
                <input type="text" className="form-control" placeholder="Username Prefix" aria-label="Username"
                       aria-describedby="addon-wrapping"
                       ref={name}/>
                <button className="btn btn-outline-dark"
                        onClick={e => {
                            setUsernamePrefix(name.current.value)
                            setPage(0)
                        }}
                >Search
                </button>
            </div>
            <div className="row justify-content-center">
                {selectHasSubjectOrNot('col-12 col-sm-8 col-md-6 col-lg-3')}
            </div>

            <table className="table table-striped table-hover text-center">
                <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Name</th>
                    <th scope="col">National Number</th>
                    <th scope="col">Fisnish Its Subject ?</th>
                    <th scope='col'>Delete</th>
                </tr>
                </thead>
                <tbody>
                {data?.map((d, i) => <tr key={d.id}>
                    <th scope="row">{i + page * PAGE_SIZE + 1}</th>
                    <td><Link to={'/doctor/' + d.id} state={{from: loc}}>{d.username}</Link></td>
                    <td>{d.nationalNumber}</td>
                    <td>{d.isComplete ?
                        <FontAwesomeIcon icon={faCheck} className={'text-primary'}/> :
                        <FontAwesomeIcon icon={faX} className={'text-danger'}/>
                    }</td>
                    <td><FontAwesomeIcon icon={faTrash} className='text-danger'
                                         style={{cursor: 'pointer'}}
                                         onClick={removeHandlerOne(d.id)}/></td>
                </tr>)}
                </tbody>
            </table>

            <Pagination page={page}
                        setPage={setPage}
                        hasNext={(data?.length ?? 0) == 0 || (data?.length ?? 0) < PAGE_SIZE}
                        className={'my-3'}
            />

        </div>
    )
}

export default DoctorsList