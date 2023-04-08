import {baseApi} from "./BaseApi";

export const subjectMaterialApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        addSubjectMaterial: builder.mutation<boolean, { subjectId: number, data:FormData }>({
            query: arg => ({
                url: 'subjectmaterial/'+arg.subjectId,
                method: 'post',
                data: arg.data
            }),
            invalidatesTags: (result, error, arg) => [{type: 'subject', id: arg.subjectId}]
        }),
        deleteSubjectMaterial: builder.mutation<boolean, {id:number,subjectId:number}>({
            query: arg => ({
                url: 'subjectmaterial/' + arg.id,
                method: 'delete'
            }),
            invalidatesTags: (result, error, arg) => [{type: 'subject', id: arg.subjectId}]
        })
    })
})
export const {
    useAddSubjectMaterialMutation,
    useDeleteSubjectMaterialMutation
} = subjectMaterialApi;