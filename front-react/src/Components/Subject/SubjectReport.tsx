import {useParams} from "react-router-dom";
import {useGetSubjectReportQuery} from "../../App/Api/SubjectApi";

const SubjectReport = () => {
    const {id} = useParams()
    const {data: report, isFetching, isError, error} = useGetSubjectReportQuery(Number(id! ?? ''))
    return (
        <div>
            <h3>{JSON.stringify(report, null, '\n')}</h3>
        </div>
    );
};

export default SubjectReport;