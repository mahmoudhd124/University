import React from "react";
import { Link } from "react-router-dom";
import useAppSelector from "../../Hookes/useAppSelector";
import { logout } from "../../Feutures/Auth/authSlice";
import useAppDispatch from "../../Hookes/useAppDispatch";

const Navbar = () => {
    const token = useAppSelector(s => s.auth.token)
    const dispatch = useAppDispatch()
    const submitHandler = (e: React.FormEvent) => {
        e.preventDefault()
    }

    return (
        <nav className="navbar navbar-expand-lg bg-light">
            <div className="container-fluid">
                <Link to={'/'} className="navbar-brand">Navbar</Link>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
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
                                        <Link to={'/profile'} className="nav-link">Profile</Link>
                                    </li>
                                    <li className="nav-item text-danger nav-link" style={{ cursor: 'pointer' }}
                                        onClick={e => dispatch(logout())}>Logout</li>
                                </>)}
                        <li className="nav-item dropdown">
                            <Link to={'/'} className="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Dropdown
                            </Link>
                            <ul className="dropdown-menu">
                                <li><Link to={'/doctor/list/0'} className="dropdown-item">Doctors</Link></li>
                                <li><Link to={'/subject/list/0'} className="dropdown-item">Subject</Link></li>
                                <li>
                                    <hr className="dropdown-divider" />
                                </li>
                                <li><Link to={'/'} className="dropdown-item">Something else here</Link></li>
                            </ul>
                        </li>
                        <li className="nav-item">
                            <Link to={'/'} className="nav-link disabled">Disabled</Link>
                        </li>
                    </ul>
                    <form onSubmit={submitHandler} className="d-flex" role="search">
                        <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
                        <button className="btn btn-outline-success" type="submit">Search</button>
                    </form>
                </div>
            </div>
        </nav>
    )
}

export default Navbar