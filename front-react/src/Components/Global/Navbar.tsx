import React from "react";
import { Link } from "react-router-dom";
import useAppSelector from "../../Hookes/useAppSelector";
import { logout } from "../../Feutures/Auth/authSlice";
import useAppDispatch from "../../Hookes/useAppDispatch";
import './Navbar.scss'
const Navbar = () => {
    const token = useAppSelector(s => s.auth.token)
    const dispatch = useAppDispatch()
    const isAdmin = useAppSelector(s => s.auth.roles)?.some(r => r.toLowerCase() == 'admin')
    const styleImg = {
        width: "40px",
        height: '40px',
        borderRadius: '50%',
        display: 'inline',
        marginRight: '20px'
    };
    return (
        <nav className="navbar navbar-expand-lg">
            <div className="container-fluid">
                <Link to={'/'} className="navbar-brand">
                    <img src="../../../public/Images/logo.png" alt="logo" style={{ marginLeft: "20px" }} width={120} height={100} />
                </Link>
                <h4 className={'text'} >Quality Management System
                    <h6 className={'text'} >CS department</h6>
                </h4>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav mb-2 mb-lg-0">
                        {isAdmin && <li className="nav-item dropdown">
                            <Link to={'/'} className="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Admin Area
                            </Link>
                            <ul className="dropdown-menu">
                                <li><Link to={'/doctor/list/0'} className="dropdown-item" ><img src="../../../public/Images/doctor.png" style={styleImg} />Doctors</Link></li>
                                <li><Link to={'/subject/list/0'} className="dropdown-item" > <img src="../../../public/Images/material.png" style={styleImg} />Courses</Link></li>
                            </ul>
                        </li>}
                        {token == null ?
                            (<>
                                <li className="nav-item btn"
                                    onClick={e => dispatch(logout())}>
                                    <button className="btn m-4">Login <img src="../../../public/Images/login.png" style={{ width: '35px', height: '35px', borderRadius: '50%' }}></img></button>
                                </li>
                                {/*<li className="nav-item">*/}
                                {/*    <Link to={'/auth/reg'} className="nav-link">Signup</Link>*/}
                                {/*</li>*/}
                            </>) : (
                                <>
                                    <li className="nav-item">
                                        {!isAdmin && <Link to={'/profile'} className="nav-link">Profile</Link>}
                                    </li>
                                    <li className="nav-item btn"
                                        onClick={e => dispatch(logout())}>
                                        <button className="btn m-4">Logout <img src="../../../public/Images/login.png" style={{ width: '35px', height: '35px', borderRadius: '50%' }}></img></button>
                                    </li>
                                </>)}
                        <div>
                            <img src="../../../public/Images/colleg-logo.jpg" style={{ marginLeft: "25px", borderRadius: '50%', padding: '5px' }} width={80} height={80} />
                            <h5 className={'text'} >FCAI BSU</h5>
                        </div>
                    </ul>
                </div>
            </div>
        </nav>
    )
}

export default Navbar