import React from "react";
import {Link} from "react-router-dom";
import useAppSelector from "../../Hookes/useAppSelector";
import {logout} from "../../Feutures/Auth/authSlice";
import useAppDispatch from "../../Hookes/useAppDispatch";

const Navbar = () => {
    const token = useAppSelector(s => s.auth.token)
    const dispatch = useAppDispatch()
    const isAdmin = useAppSelector(s => s.auth.roles)?.some(r => r.toLowerCase() == 'admin')

    return (
        <nav className="navbar navbar-expand-lg bg-light">
            <div className="container-fluid">
                <Link to={'/'} className="navbar-brand">
                    <img src="../../../public/Images/logo.jpeg" alt="logo" width={50} height={40}/>
                </Link>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                        data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav mb-2 mb-lg-0 w-100">
                        <li className="nav-item dropdown">
                            <Link to={'/'} className="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown"
                                  aria-expanded="false">
                                More...
                            </Link>
                            <ul className="dropdown-menu">
                                <li><Link to={'/doctor/list/0'} className="dropdown-item">Doctors</Link></li>
                                <li><Link to={'/subject/list/0'} className="dropdown-item">Subject</Link></li>
                            </ul>
                        </li>
                        {token == null ?
                            (<>
                                <li className="nav-item">
                                    <Link to={'/auth/login'} className="nav-link">Login</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to={'/auth/reg'} className="nav-link">Signup</Link>
                                </li>
                            </>) : (
                                <>
                                    <li className="nav-item">
                                        {!isAdmin && <Link to={'/profile'} className="nav-link">Profile</Link>}
                                    </li>
                                    <li className="nav-item ms-lg-auto btn btn-outline-danger col-12 col-sm-3 col-md-1" style={{cursor: 'pointer'}}
                                        onClick={e => dispatch(logout())}>Logout
                                    </li>
                                </>)}
                    </ul>
                </div>
            </div>
        </nav>
    )
}

export default Navbar