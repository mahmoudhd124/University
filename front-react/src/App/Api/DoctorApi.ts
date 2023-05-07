import {AddDoctorModel} from "../../Models/Doctor/AddDoctorModel";
import {DoctorForPageModel} from "../../Models/Doctor/DoctorForPageModel";
import {DoctorModel} from "../../Models/Doctor/DoctorModel";
import {baseApi} from "./BaseApi";
import {EditDoctorModel} from "../../Models/Doctor/EditDoctorModel";

export const DoctorApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        addDoctor: builder.mutation<boolean, AddDoctorModel>({
            query: args => ({
                url: 'doctor',
                method: 'post',
                body: args
            }),
            invalidatesTags: ['doctor']
        }),
        getDoctorPage: builder.query<DoctorForPageModel[], { pageIndex: number, pageSize: number, usernamePrefix: string }>({
            query: args => ({
                url: `doctor/${args.pageIndex}/${args.pageSize}/${args.usernamePrefix}`
            }),
            providesTags: (result = []) => [
                'doctor',
                ...result.map(({id}) => ({type: 'doctor' as const, id}))
            ]
        }),
        getDoctor: builder.query<DoctorModel, string>({
            // query: arg => 'doctor/' + arg,
            query: arg => ({url: 'doctor/' + arg}),
            providesTags: (result, error, arg) => [{type: 'doctor', id: arg == '' ? 'ME' : arg}]
        }),
        getDoctorEditInfo: builder.query<EditDoctorModel, string>({
            query: arg => 'doctor/geteditinfo/' + arg,
            providesTags: (result, error, arg) => [{type: 'doctor', id: arg}]
        }),
        editDoctor: builder.mutation<boolean, EditDoctorModel>({
            query: args => ({
                url: 'doctor',
                method: 'put',
                body: args
            }),
            invalidatesTags: (result, error, arg) => [
                {type: 'doctor', id: arg.id},
                {type: 'doctor', id: 'ME'}
            ]
        }),
        deleteDoctor: builder.mutation<boolean, string>({
            query: arg => ({
                url: 'doctor/' + arg,
                method: 'delete'
            }),
            invalidatesTags: (result, error, arg) => [{type: 'doctor', id: arg}]
        }),
        assignDoctorToSubject: builder.mutation<boolean, { did: string, sid: number }>({
            query: args => '/doctor/AssignToSubject/' + args.did + '/' + args.sid,
            invalidatesTags: (result, error, args) => [
                {type: 'subject', id: args.sid},
                {type: 'doctor', id: args.did}
            ]
        })
    })
})
export const {
    useAddDoctorMutation,
    useGetDoctorPageQuery,
    useGetDoctorQuery,
    useGetDoctorEditInfoQuery,
    useEditDoctorMutation,
    useDeleteDoctorMutation,
    useAssignDoctorToSubjectMutation
} = DoctorApi