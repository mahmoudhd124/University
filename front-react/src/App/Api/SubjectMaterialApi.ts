import {baseApi} from "./BaseApi";

export const subjectMaterialApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        addSubjectMaterial: builder.mutation<boolean, FormData>({
            query: arg => ({
                    url: 'subjectMaterial',
                    method: 'post',
                    data: arg,
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
            }),
            invalidatesTags: (result, error, arg) => [{type: 'subject', id: +arg.get('subjectId')!}]
        }),
        deleteSubjectMaterial: builder.mutation<boolean, { id: number, subjectId: number }>({
            query: arg => ({
                url: 'subjectMaterial/' + arg.id,
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