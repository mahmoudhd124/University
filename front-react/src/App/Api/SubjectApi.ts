import {baseApi} from "./BaseApi";
import {SubjectModel} from "../../Models/Subject/SubjectModel";
import {SubjectForPageModel} from "../../Models/Subject/SubjectForPageModel";
import {AddSubjectModel} from "../../Models/Subject/AddSubjectModel";

export const subjectApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getSubjectByCode: builder.query<SubjectModel, number>({
            query: arg => 'subject/' + arg,
            providesTags: (result) => [{type: 'subject', id: result!.id}]
        }),
        getSubjectPage: builder.query<SubjectForPageModel[],
            { pageIndex: number, pageSize: number, department: string | undefined, year: number | undefined }>({
            query: args => ({
                url: `subject/${args.pageIndex}/${args.pageSize}`,
                params: {
                    'department': args.department,
                    'year': args.year
                }
            }),
            providesTags: (result = []) => [
                'subject',
                ...result.map(({id}) => ({type: 'subject' as const, id}))
            ]
        }),
        addSubject: builder.mutation<boolean, AddSubjectModel>({
            query: arg => ({
                url: 'subject',
                method: 'post',
                body: arg
            }),
            invalidatesTags: ['subject']
        }),
        deleteSubject: builder.mutation<boolean, number>({
            query: arg => ({
                url: 'subject/' + arg,
                method: 'delete'
            }),
            invalidatesTags: (result, error, arg ) => [{type: 'subject', id: arg}]
        }),
        deleteAssignedDoctor: builder.mutation<boolean, number>({
            query: arg => ({
                url: 'subject/DeleteAssignedDoctor/' + arg,
                method: 'delete'
            }),
            invalidatesTags: (result, error, args) => [
                'doctor',
                {type: 'subject', id: args}
            ]
        })
    })
})
export const {
    useGetSubjectByCodeQuery,
    useGetSubjectPageQuery,
    useAddSubjectMutation,
    useDeleteSubjectMutation,
    useDeleteAssignedDoctorMutation
} = subjectApi