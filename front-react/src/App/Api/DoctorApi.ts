import {AddDoctorModel} from "../../Models/Doctor/AddDoctorModel";
import {DoctorForPageModel} from "../../Models/Doctor/DoctorForPageModel";
import {DoctorModel} from "../../Models/Doctor/DoctorModel";
import {baseApi} from "./baseApi";
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
            providesTags: (result = [], error, args) => [
                'doctor',
                ...result.map(({id}) => ({type: 'doctor' as const, id}))
            ]
        }),
        getDoctor: builder.query<DoctorModel, string>({
            query: arg => 'doctor/' + arg,
            providesTags: (result, error, arg) => [{type: 'doctor', id: arg}]
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
            invalidatesTags: (result, error, arg, meta) => [{type: 'doctor', id: arg.id}]
        }),
        deleteDoctor: builder.mutation<boolean, string>({
            query: arg => ({
                url: 'doctor/' + arg,
                method: 'delete'
            }),
            invalidatesTags: (result, error, arg) => [{type: 'doctor', id: arg}]
        })
    })
})
export const {
    useAddDoctorMutation,
    useGetDoctorPageQuery,
    useGetDoctorQuery,
    useGetDoctorEditInfoQuery,
    useEditDoctorMutation,
    useDeleteDoctorMutation
} = DoctorApi