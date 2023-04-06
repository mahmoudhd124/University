import React, {useRef, useState} from 'react'
import {Link, useLocation,  useParams} from 'react-router-dom'
import {useDeleteDoctorMutation, useGetDoctorPageQuery} from '../../App/Api/DoctorApi'
import {Alert} from 'react-bootstrap'
import useGetAppError from '../../Hookes/useGetAppError'
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'
import {faTrash} from '@fortawesome/free-solid-svg-icons'

const PAGE_SIZE = 10
const DoctorsList = () => {
    const {pageIndex} = useParams()
    const [page, setPage] = useState(Number(pageIndex ?? '0'))
    const [usernamePrefix, setUsernamePrefix] = useState('')
    const loc = useLocation()
    const name = useRef() as React.MutableRefObject<HTMLInputElement>
    const [idToRevmoe, setIdToRemove] = useState<string | null>(null)
    const twoStepRemoveScreen = useRef() as React.MutableRefObject<HTMLDivElement>

    const {data, isError, error } =
        useGetDoctorPageQuery({pageIndex: page, pageSize: PAGE_SIZE, usernamePrefix})

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

            <table className="table table-striped table-hover text-center">
                <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Name</th>
                    <th scope="col">National Number</th>
                    <th scope='col'>Delete</th>
                </tr>
                </thead>
                <tbody>
                {data?.map((d, i) => <tr key={d.id}>
                    <th scope="row">{i + page * PAGE_SIZE + 1}</th>
                    <td><Link to={'/doctor/' + d.id} state={{from: loc}}>{d.username}</Link></td>
                    <td>{d.nationalNumber}</td>
                    <td><FontAwesomeIcon icon={faTrash} className='text-danger'
                                         style={{cursor: 'pointer'}}
                                         onClick={removeHandlerOne(d.id)}/></td>
                </tr>)}
                </tbody>
            </table>
            <nav className="row justify-content-center align-items-baseline"
                 onMouseMove={e => e.preventDefault()}
            >
                <ul className="pagination col-10 col-sm-8 col-md-6 justify-content-center">
                    <li className={`page-item ${page == 0 && 'disabled'}`}>
                        <button className={'page-link'} onClick={e => setPage(p => p - 1)}>dec</button>
                    </li>
                    <li className="page-item">
                        <button className="page-link">{page}</button>
                    </li>
                    <li className="page-item active" aria-current="page">
                        <button className="page-link">{page + 1}</button>
                    </li>
                    <li className="page-item">
                        <button className="page-link">{page + 2}</button>
                    </li>
                    <li className={`page-item ${((data?.length ?? 0) == 0 || (data?.length ?? 0) < PAGE_SIZE) && 'disabled'}`}>
                        <button className={'page-link'} onClick={e => setPage(p => p + 1)}>inc</button>
                    </li>
                </ul>
            </nav>

        </div>
    )
}

export default DoctorsList