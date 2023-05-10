import React, {useRef, useState} from 'react';
import {Link, useLocation, useNavigate, useParams} from "react-router-dom";
import {Alert} from "react-bootstrap";
import useGetAppError from "../../Hookes/useGetAppError";
import {useDeleteSubjectMutation, useGetSubjectPageQuery} from "../../App/Api/SubjectApi";
import Pagination from "../Pagination";
import SubjectForList from "./SubjectForList";

const SubjectList = () => {
    const PAGE_SIZE = 10
    const {pageIndex} = useParams()
    const [page, setPage] = useState(Number(pageIndex ?? '0'))
    const [department, setDepartment] = useState<string>()
    const [year, setYear] = useState<number>()
    const [namePrefix, setNamePrefix] = useState<string>()
    const departmentFilter = useRef() as React.MutableRefObject<HTMLInputElement>
    const yearFilter = useRef() as React.MutableRefObject<HTMLInputElement>
    const nameFilter = useRef() as React.MutableRefObject<HTMLInputElement>
    const navigator = useNavigate()
    const loc = useLocation()

    const {data, isError, error} =
        useGetSubjectPageQuery({pageIndex: page, pageSize: PAGE_SIZE, department, year,namePrefix})

    const [remove, removeResult] = useDeleteSubjectMutation()
    const [idToRemove, setIdToRemove] = useState<number | null>(null)
    const removeHandlerOne = (id: number) => {
        return (e: React.MouseEvent) => {
            e.preventDefault()
            e.stopPropagation()
            window.scroll({top:0,behavior:'smooth'})
            setIdToRemove(id)
        }
    }

    const removeHandlerTwo = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        remove(idToRemove!)
        setIdToRemove(null)
    }

    const returnBack = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        setIdToRemove(null)
    }

    return (
        <>
            <div className='container'>
                {isError && <Alert variant='danger'>{useGetAppError(error)?.message}</Alert>}

                {idToRemove && <div
                    className='border border-3 roudned rounded-3 mx-auto p-3'>
                    <h3 className='text-center'>Are You Sure You Want To
                        Delete {data?.filter(d => d.id == idToRemove)[0].name}</h3>
                    <div className="row justify-content-center gap-4">
                        <div className="col-4">
                            <button onClick={removeHandlerTwo} className='btn btn-outline-danger'>Remove Any Way
                            </button>
                        </div>

                        <div className="col-4">
                            <button onClick={returnBack} className='btn btn-outline-primary'>Return Back</button>
                        </div>
                    </div>
                </div>}

                <div className='row justify-content-center'>
                    <div className='col-8 col-md-5'>
                        <Link className='btn btn-outline-dark w-100 my-3' to={'/subject/add'} state={{from: loc}}>
                            Add Subject
                        </Link>
                    </div>
                </div>
                <h3>Subjects!</h3>

                <div className="input-group flex-nowrap my-3">
                    <span className="input-group-text" id="addon-wrapping">Name Prefix</span>
                    <input type="text" className="form-control" placeholder="xxyyzz....." aria-label="Username"
                           aria-describedby="addon-wrapping"
                           ref={nameFilter}/>
                </div>

                <div className="input-group flex-nowrap my-3">
                    <span className="input-group-text" id="addon-wrapping">Department</span>
                    <input type="text" className="form-control" placeholder="cs,is,mm,it,..." aria-label="Username"
                           aria-describedby="addon-wrapping"
                           ref={departmentFilter}/>
                </div>

                <div className="input-group flex-nowrap my-3">
                    <span className="input-group-text" id="addon-wrapping">Year</span>
                    <input type="number" className="form-control" placeholder="1,2,3,4,..." aria-label="Username"
                           aria-describedby="addon-wrapping"
                           ref={yearFilter}/>
                </div>
                <div className="text-center">
                    <button className="btn btn-outline-dark"
                            onClick={e => {
                                setDepartment(departmentFilter.current.value)
                                setYear(+yearFilter.current.value == 0 ? undefined : +yearFilter.current.value)
                                setNamePrefix(nameFilter.current.value)
                                setPage(0)
                            }}
                    >Search
                    </button>
                </div>

            </div>
            <div style={{backgroundColor: '#f3f0f0'}} className={'my-2'}>
                <div className={'container'}>
                    <div className="row justify-content-center justify-content-sm-around gap-3">
                        {data?.map(s => <SubjectForList key={s.id}
                                                        subject={s}
                                                        onClickHandler={e => navigator('/subject/' + s.code, {state: {from: loc}})}
                                                        onDelete={removeHandlerOne(s.id)}
                        />)}
                    </div>

                    <Pagination page={page}
                                setPage={setPage}
                                hasNext={(data?.length ?? 0) == 0 || (data?.length ?? 0) < PAGE_SIZE}
                                className={'my-3'}
                    />
                </div>
            </div>
        </>
    )
};

export default SubjectList;