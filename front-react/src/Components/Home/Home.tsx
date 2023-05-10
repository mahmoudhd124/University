import './Home.scss'
import {useNavigate} from "react-router-dom";

const Home = () => {
    const navigator = useNavigate()
    return (
        <section className={'main'}>
            <div className="container">
                <img src="../../../public/Images/download.png" className={'bg-img'} alt=""/>
                {/*<img src="../../../public/Images/colleg-logo.jpg" className={'logo'} draggable={false} alt=""/>*/}
                {/*<img src="../../../public/Images/logo.jpeg" className={'site-logo'} draggable={false} alt=""/>*/}
                <div className="bg"></div>
                <h3>University Doctor Management</h3>
                <h5 className={'w-75'}>Welcome to our site for managing doctors at the university. Our platform allows the director to
                    easily manage and organize doctors, while ensuring easy access. We strive to provide you with a
                    smooth and efficient experience.</h5>
                <button className={'button2'}
                        onClick={e => navigator('/auth/login')}
                >Get Started
                </button>
            </div>
        </section>
    );
};

export default Home;