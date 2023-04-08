import * as Yup from 'yup'
import { useEffect } from 'react'
import { AddDoctorModel } from '../../Models/Doctor/AddDoctorModel'
import { useFormik } from 'formik'
import { useAddDoctorMutation } from '../../App/Api/DoctorApi'
import useGetAppError from '../../Hookes/useGetAppError'

const AddDoctor = () => {
    const [add, addResult] = useAddDoctorMutation()
    //@ts-ignore
    const formik = useFormik<AddDoctorModel & { confirmPassword: string }>({
        initialValues: {
            firstName: '',
            lastName: '',
            username: '',
            phoneNumber: '',
            email: '',
            password: '',
            confirmPassword: '',
            nationalNumber: ''
        },
        validationSchema: Yup.object({
            firstName: Yup.string().required('first name is required'),
            lastName: Yup.string().required('last name is required'),
            username: Yup.string().required('username is required'),
            phone: Yup.string().required('phone is required'),
            email: Yup.string().required('email is required'),
            password: Yup.string()
                .test({
                    message: 'The password must has more than 8 characters and at least one small and one capital character and one non-alphabitic character and at least one number',
                    test: (v) =>
                        new RegExp("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").test(v!)
                })
                .required('password is required'),
            confirmPassword: Yup.string()
                .test({
                    message: 'the confirm password is not the same as the password',
                    test: (v) => formik.values.password == v
                })
                .required('confirm password is required')
        }),
        onSubmit: (values: AddDoctorModel) => {
            add(values)
        }
    })

    useEffect(() => {
        if (formik.values.username.trim().length == 0)
            return

        fetch('https://localhost:7108/api/auth/isvalidusername/' + formik.values.username)
            .then(r => r.json())
            .then(r => {
                if (r === false) {
                    console.log({ username: formik.values.username, r });
                    formik.setErrors({ ...formik.errors, username: 'the username is already token' })
                }
            });

    }, [formik.values.username])

    return (
        <div className="container">
            <form onSubmit={formik.handleSubmit}
                className={'d-flex flex-column p-3 mx-auto border border-3 rounded rounded-3' +
                    ' shadow' +
                    ' gap-2'}
                style={{ height: 'fit-content' }}>
                <p className={'text-center h5 text-danger'}>{addResult.isError && useGetAppError(addResult.error)?.message}</p>
                <p className={'text-center h5 text-primary'}>{addResult.isSuccess && 'Registration Complete Successfully'}</p>
                <div className={'row d-flex justify-content-between align-items-end'}>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.firstName && formik.errors.firstName) ?
                            <label htmlFor="fname"
                                className={'col-form-label text-danger'}>{formik.errors.firstName}</label> :
                            <label htmlFor="fname" className={'col-form-label'}>First Name</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.firstName && formik.errors.firstName) && 'border-1 border-danger'}`}
                            id={'fname'}
                            name={'firstName'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.firstName}
                        />
                    </div>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.lastName && formik.errors.lastName) ?
                            <label htmlFor="lname"
                                className={'col-form-label text-danger'}>{formik.errors.lastName}</label> :
                            <label htmlFor="lname" className={'col-form-label'}>Last Name</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.lastName && formik.errors.lastName) && 'border-1' +
                                ' border-danger'}`}
                            id={'lname'}
                            name={'lastName'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.lastName}
                        />
                    </div>
                </div>

                <div className={'row d-flex justify-content-between align-items-end'}>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.username && formik.errors.username) ?
                            <label htmlFor="uname"
                                className={'col-form-label text-danger'}>{formik.errors.username}</label> :
                            <label htmlFor="uname" className={'col-form-label'}>Username</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.username && formik.errors.username) && 'border-1' +
                                ' border-danger'}`}
                            id={'uname'}
                            name={'username'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.username}
                        />
                    </div>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.phone && formik.errors.phone) ?
                            <label htmlFor="phone"
                                className={'col-form-label text-danger'}>{formik.errors.phone}</label> :
                            <label htmlFor="phone" className={'col-form-label'}>Phone</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.phone && formik.errors.phone) && 'border-1' +
                                ' border-danger'}`}
                            id={'phone'}
                            name={'phoneNumber'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.phoneNumber}
                        />
                    </div>
                </div>

                <div className={'row d-flex justify-content-between align-items-end'}>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.nationalNumber && formik.errors.nationalNumber) ?
                            <label htmlFor="nationNum"
                                className={'col-form-label text-danger'}>{formik.errors.nationalNumber}</label> :
                            <label htmlFor="nationNum" className={'col-form-label'}>National Number</label>}
                        <input
                            type="text"
                            className={`form-control ${(formik.touched.nationalNumber && formik.errors.nationalNumber) && 'border-1' +
                                ' border-danger'}`}
                            id={'nationNum'}
                            name={'nationalNumber'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.nationalNumber}
                        />
                    </div>
                </div>

                <div>
                    {(formik.touched.email && formik.errors.email) ?
                        <label htmlFor="email"
                            className={'col-form-label text-danger'}>{formik.errors.email}</label> :
                        <label htmlFor="email" className={'col-form-label'}>Email</label>}
                    <input
                        type="email"
                        className={`form-control ${(formik.touched.email && formik.errors.email) && 'border-1' +
                            ' border-danger'}`}
                        id={'email'}
                        name={'email'}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        value={formik.values.email}
                    />
                </div>

                <div className={'row d-flex justify-content-between align-items-start'}>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.password && formik.errors.password) ?
                            <label htmlFor="password"
                                className={'col-form-label text-danger'}>{formik.errors.password}</label> :
                            <label htmlFor="password" className={'col-form-label'}>Password</label>}
                        <input
                            type="password"
                            className={`form-control ${(formik.touched.password && formik.errors.password) && 'border-1' +
                                ' border-danger'}`}
                            id={'pass'}
                            name={'password'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.password}
                        />
                    </div>
                    <div className={'col-12 col-sm-6'}>
                        {(formik.touched.confirmPassword && formik.errors.confirmPassword) ?
                            <label htmlFor="con-pass"
                                className={'col-form-label text-danger'}>{formik.errors.confirmPassword}</label> :
                            <label htmlFor="con-pass" className={'col-form-label'}>Confirm Password</label>}
                        <input
                            type="password"
                            className={`form-control ${(formik.touched.confirmPassword && formik.errors.confirmPassword) && 'border-1' +
                                ' border-danger'}`}
                            id={'con-pass'}
                            name={'confirmPassword'}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.confirmPassword}
                        />
                    </div>
                </div>

                <button className={'btn btn-primary w-50 align-self-center mt-3'}>Signup</button>
            </form >
        </div>
    );
}

export default AddDoctor