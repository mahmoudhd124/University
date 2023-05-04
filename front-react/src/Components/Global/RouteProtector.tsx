import { Navigate, Outlet, useLocation } from "react-router-dom";
import useAppSelector from "../../Hookes/useAppSelector";
import Forbidden403 from "./Forbidden/Forbidden403";

const RouteProtector = (props: { allowedRoles: string[] }) => {
    const { token, roles } = useAppSelector(s => s.auth)
    const loc = useLocation()

    return (
        <>
            {token ? (
                <>
                    {props.allowedRoles.length > 0 ?
                        roles?.some(r =>
                            props.allowedRoles.some(ar =>
                                ar.trim().toLowerCase() === r.trim().toLowerCase())) ? <Outlet /> : <Forbidden403/>
                        :
                        <>
                            <Outlet />
                        </>
                    }
                </>) : (
                <>
                    <Navigate to={'/auth/login'} state={{ from: loc }} />
                </>)}
        </>
    );
};

export default RouteProtector;