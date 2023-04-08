import {SerializedError} from "@reduxjs/toolkit"
import {FetchBaseQueryError} from "@reduxjs/toolkit/dist/query"
import {AppError} from "../App/Api/BaseApi"

const useGetAppError = (error: FetchBaseQueryError | SerializedError | undefined) => {

    return error == undefined ? error : (error as FetchBaseQueryError).data as AppError
}

export default useGetAppError