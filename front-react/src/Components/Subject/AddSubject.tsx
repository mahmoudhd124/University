import React from 'react';
import {useFormik} from "formik";
import * as Yup from "yup";
import useGetAppError from "../../Hookes/useGetAppError";
import {AddSubjectModel} from "../../Models/Subject/AddSubjectModel";
import {useAddSubjectMutation} from "../../App/Api/SubjectApi";

const AddSubject = () => {
    const [add, addResult] = useAddSubjectMutation()
    const formik = useFormik<AddSubjectModel>({
        initialValues: {
            name: '',
            code: 0,
            department: '',
            hours: 0
        },
        validationSchema: Yup.object({
            name: Yup.string().required('This Field Is Required'),
            code: Yup.number().required('This Field Is Required'),
            department: Yup.string().required('This Field Is Required'),
            hours: Yup.number().required('This Field Is Required')
        }),
        onSubmit: (values) => {
            add(values)
        }
    })

    return (
        <div className="container">
            <form onSubmit={formik.handleSubmit}
                  className={'d-flex flex-column p-3 mx-auto border border-3 rounded rounded-3' +
                      ' shadow' +
                      ' gap-2 my-5'}
                  style={{height: 'fit-content'}}>
                <p className={'text-center h5 text-danger'}>{addResult.isError && useGetAppError(addResult.error)?.message}</p>
                <p className={'text-center h5 text-primary'}>{addResult.isSuccess && 'Subject Added Successfully'}</p>
                <div className={'row d-flex justify-content-between align-items-end'}>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.name && formik.errors.name) ?
                            <label htmlFor="name"
                                   className={'col-form-label text-danger'}>{formik.errors.name}</label> :
                            <label htmlFor="name" className={'col-form-label'}>Name</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.name && formik.errors.name) && 'border-1 border-danger'}`}
                            id={'name'}
                            name={'name'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.name}
                        />
                    </div>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.department && formik.errors.department) ?
                            <label htmlFor="department"
                                   className={'col-form-label text-danger'}>{formik.errors.department}</label> :
                            <label htmlFor="department" className={'col-form-label'}>Department</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.department && formik.errors.department) && 'border-1' +
                            ' border-danger'}`}
                            id={'department'}
                            name={'department'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.department}
                        />
                    </div>
                </div>

                <div className={'row d-flex justify-content-between align-items-end'}>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.code && formik.errors.code) ?
                            <label htmlFor="code"
                                   className={'col-form-label text-danger'}>{formik.errors.code}</label> :
                            <label htmlFor="code" className={'col-form-label'}>Code</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.code && formik.errors.code) && 'border-1' +
                            ' border-danger'}`}
                            id={'code'}
                            name={'code'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.code}
                        />
                    </div>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.hours && formik.errors.hours) ?
                            <label htmlFor="hours"
                                   className={'col-form-label text-danger'}>{formik.errors.hours}</label> :
                            <label htmlFor="hours" className={'col-form-label'}>Hours</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.hours && formik.errors.hours) && 'border-1' +
                            ' border-danger'}`}
                            id={'hours'}
                            name={'hours'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.hours}
                        />
                    </div>
                </div>

                <button className={'btn btn-primary w-50 align-self-center mt-3'}>Add</button>
            </form>
        </div>);
};

export default AddSubject;