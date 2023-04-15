import { Route, Routes, useLocation, useNavigate } from 'react-router-dom'
import Layout from './Components/Global/Layout'
import './app.css'
import Profile from "./Components/Profile/Profile";
import Login from "./Components/Auth/Login";
import Signup from "./Components/Auth/Signup";
import AuthLayout from "./Components/Auth/AuthLayout";
import RouteProtector from './Components/Global/RouteProtector';
import useRefreshToken from './Hookes/useRefreshToken';
import { logout, setCredentials } from './Feutures/Auth/authSlice';
import TokenModel from './Models/Auth/TokenModel';
import useAppDispatch from './Hookes/useAppDispatch';
import { useEffect } from 'react';
import AddDoctor from './Components/Doctor/AddDoctor'
import DoctorsList from './Components/Doctor/DoctorsList';
import DoctorPage from './Components/Doctor/DoctorPage';
import EditDoctor from "./Components/Doctor/EditDoctor";
import SubjectPage from "./Components/Subject/SubjectPage";
import SubjectList from "./Components/Subject/SubjectList";
import AddSubject from "./Components/Subject/AddSubject";
import EditSubject from "./Components/Subject/EditSubject";

const App = () => {
    const stayLogin = JSON.parse(localStorage.getItem('stayLogin') ?? 'false')
    const dispatch = useAppDispatch()
    const loc = useLocation()
    const navigator = useNavigate()

    useEffect(() => {
        if (stayLogin) {
            (async () => {
                const refresh = useRefreshToken()
                const data = await refresh()
                if (data) {
                    console.log('login done');
                    dispatch(setCredentials(data as TokenModel))
                    navigator(loc.pathname)
                } else {
                    console.log('login faild');
                    dispatch(logout())
                }
            })()
        }
    }, [])

    return (
        <Routes>
            <Route path='/' element={<Layout />}>
                <Route element={<RouteProtector allowedRoles={[]} />}>
                    <Route path={'profile'} element={<Profile />} />
                </Route>

                //auth routes
                <Route path={'auth'} element={<AuthLayout />}>
                    <Route path={'login'} element={<Login />} />
                    <Route path={'reg'} element={<Signup />} />
                </Route>

                //doctor routes
                <Route path={'doctor'} element={<RouteProtector allowedRoles={['admin']} />}>
                    <Route path={'Add'} element={<AddDoctor />} />
                    <Route path={'List/:pageIndex'} element={<DoctorsList />} />
                </Route>

                <Route path={'doctor'} element={<RouteProtector allowedRoles={['doctor']} />}>
                    <Route path={'Edit/:id'} element={<EditDoctor />} />
                </Route>

                <Route path={'doctor'} element={<RouteProtector allowedRoles={[]} />}>
                    <Route index element={<DoctorPage />} />
                    <Route path={':id'} element={<DoctorPage />} />
                </Route>

                //subject routes
                <Route path={'subject'} element={<RouteProtector allowedRoles={['admin']} />}>
                    <Route path={'Add'} element={<AddSubject />} />
                    <Route path={'List/:pageIndex'} element={<SubjectList />} />
                    <Route path={'Edit/:code'} element={<EditSubject />} />
                </Route>

                <Route path={'subject'} element={<RouteProtector allowedRoles={[]} />}>
                    <Route path={':code'} element={<SubjectPage />} />
                </Route>


            </Route>

            //all athore routes
            <Route path='/*' element={<h1>NO PAGE</h1>} />
        </Routes>

    )
}

export default App