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
                <Route element={<RouteProtector allwedRoles={[]} />} >
                    <Route path={'profile'} element={<Profile />} />
                </Route>


                <Route path={'auth'} element={<AuthLayout />}>
                    <Route path={'login'} element={<Login />} />
                    <Route path={'reg'} element={<Signup />} />
                </Route>
            </Route>
        </Routes >

    )
}

export default App
