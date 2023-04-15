import { BASE_URL, baseApi} from "./BaseApi";
import {AppDispatch} from "../store";

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

//todo handle it with better way, may be with rtk query with axios
export const useAddSubjectMaterialMutation = (token: string, dispatch: AppDispatch) => {
    return async (data: FormData) => {
        dispatch(baseApi.util.invalidateTags([{type: 'subject', id: +data.get('subjectId')!}]))
        return await fetch(BASE_URL + 'subjectmaterial', {
            method: 'POST',
            body: data,
            headers: {
                'authorization': `Bearer ${token}`
            }
        });
    }
}
export const {
    // useAddSubjectMaterialMutation,
    useDeleteSubjectMaterialMutation
} = subjectMaterialApi;