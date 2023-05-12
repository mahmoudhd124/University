import {useGetSendMessageQuery} from "../../App/Api/MessageApi";
import {useState} from "react";
import Pagination from "../Pagination";
import TimeAgo from "../Global/TimeAgo";

const SentMessages = () => {
    const pageSize = 10
    const [page, setPage] = useState(0)
    const {data, isFetching, isError, error} = useGetSendMessageQuery({pageSize, pageIndex: page})

    if (isFetching)
        return <h3>Loading...</h3>

    return (
        <>
            <table className="table table-hover">
                <thead>
                <tr>
                    <th>To</th>
                    <th>Titel</th>
                    <th>#</th>
                </tr>
                </thead>
                <tbody>
                {data?.map(m => <tr key={m.id}>
                    <td>{m.receiverUsername}</td>
                    <td>{m.title}</td>
                    <td><TimeAgo timestamp={m.date}/></td>
                </tr>)}
                </tbody>
            </table>
            <Pagination page={page} setPage={setPage} hasNext={(data?.length ?? -1) < pageSize - 1}/>
        </>
    );
};

export default SentMessages;