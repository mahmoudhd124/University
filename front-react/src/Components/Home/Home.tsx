import './Home.scss'
import {useNavigate} from "react-router-dom";

const Home = () => {
    const navigator = useNavigate()
    return (
        <section className={'main'}>
            <div className="container">
                <div className="row my-5 gap-5">
                    <div className='col-12 col-md-3'>
                        <div className="text-center">
                            <img src="../../../public/Images/logo.jpg" className={'logo'} draggable={false} alt=""/>
                            <h3>Quality Management System</h3>
                            <button className={'button2'}
                                    onClick={e => navigator('/auth/login')}
                            >Get Started
                            </button>
                        </div>
                    </div>

                    <div className='col-12 col-md-8'>
                        <p className={'text-center'}>
                            <h4>
                                :تم انشاء هذا النظام برعايه
                            </h4>
                            <p className={'mb-0'}>
                                الاستاذ الدكتور / منصور حسن - رئيس الجامعة
                            </p>
                            الاستاذ الدكتور / محمد قايد - عميد الكليه
                            <p className={'mb-0'}>
                                الاستاذ الدكتور / أحمد النجار - رئيس قسم علوم الحاسب
                            </p>
                            <p className={'mb-0'}>
                                البشمهندسة / هبه محمد - المعيد او المدرس المساعد بقسم علوم الحاسب
                            </p>
                            <p className={'mb-0'}>
                                الاستاذ / هشام محمد - مدير الكليه
                            </p>
                            <hr/>
                            <h5>
                                :وعمل كل من طلاب الفرقه الثالثه قسم علوم الحاسب للعام الدراسى 2022- 2023
                            </h5>
                            <p className={'mb-0'}>
                                محمود احمد ناصر محمود معوض
                            </p>
                            <p className={'mb-0'}>
                                مصطفى محمود عماره عبد الفتاح
                            </p>
                            <p className={'mb-0'}>
                                ريهام محمد أحمد عبدالله
                            </p>
                            <p className={'mb-0'}>
                                روضه احمد صلاح
                            </p>
                            <p className={'mb-0'}>
                                زهور ضياءالدين طلعت وزيرى
                            </p>
                            <p className={'mb-0'}>
                                سما عبد العظيم ادهم مبروك
                            </p>

                        </p>
                    </div>
                </div>

                <img src="../../../public/Images/background.jpg" className={'bg-img'} alt=""/>
                {/*<img src="../../../public/Images/colleg-logo.jpg" className={'logo'} draggable={false} alt=""/>*/}
                {/*<img src="../../../public/Images/logo.jpeg" className={'site-logo'} draggable={false} alt=""/>*/}
                <div className="bg"></div>
            </div>
        </section>
    );
};

export default Home;