import {useGetSendMessageQuery} from "../../App/Api/MessageApi";
import {useState} from "react";
import Pagination from "../Pagination";
import {Link} from "react-router-dom";

const SentMessages = () => {
    const pageSize = 10
    const [page, setPage] = useState(0)
    const {data, isFetching, isError, error} = useGetSendMessageQuery({pageSize, pageIndex: page})

    if (isFetching)
        return <h3>Loading...</h3>

    return (
        <>
            <table className="table table-hover">
                <tbody>
                <tr>
                    <td className="mail-select">
                        <label className="cr-styled">
                            <input type="checkbox"/><i className="fa"></i>
                        </label>
                    </td>
                    <td className="mail-rateing">
                        <i className="fa fa-star"></i>
                    </td>
                    <td>
                        <a href="##email-read.html">Google Inc</a>
                    </td>
                    <td>
                        <a href="##email-read.html"><i
                            className="fa fa-circle text-info m-r-15"></i>Lorem ipsum dolor sit
                            amet, consectetuer adipiscing elit</a>
                    </td>
                    <td>
                        <i className="fa fa-paperclip"></i>
                    </td>
                    <td className="text-right">
                        07:23 AM
                    </td>
                </tr>
                </tbody>
            </table>
            <Pagination page={page} setPage={setPage} hasNext={(data?.length ?? -1) < pageSize - 1}/>
        </>
    )
        ;
};

export default SentMessages;