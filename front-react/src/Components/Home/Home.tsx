import './Home.scss'
import { useNavigate } from "react-router-dom";

const Home = () => {
    const navigator = useNavigate()
    return (
        <section className={'main'}>
            <div className="container">

                <div className='split left'>
                    <div className="centered">
                        <img src="../../../public/Images/logo.jpg" className={'logo'} draggable={false} alt="" />
                        <h3>Quality Management System</h3>
                        <button className={'button2'}
                            onClick={e => navigator('/auth/login')}
                        >Get Started
                        </button>
                    </div>
                </div>
                <div className='split right' >
                    <div className="centered">

                        <p>
                            :تم انشاء هذا النظام برعايه
                            <br />
                            الاستاذ الدكتور / منصور حسن - رئيس الجامعة
                            <br />
                            الاستاذ الدكتور / محمد قايد - عميد الكليه
                            <br />
                            الاستاذ الدكتور / أحمد النجار - رئيس قسم علوم الحاسب
                            <br />
                            البشمهندسة / هبه محمد - المعيد او المدرس المساعد بقسم علوم الحاسب
                            <br />
                            الاستاذ / هشام محمد - مدير الكليه
                            <br />
                            <br />
                            :وعمل كل من طلاب الفرقه الثالثه قسم علوم الحاسب للعام الدراسى 2022- 2023
                            <br />
                            روضه أحمد صلاح عبدالسلام
                            <br />
                            ريهام محمد أحمد عبدالله
                            <br />
                            زهور ضياءالدين طلعت وزيري
                            <br />
                            زياد احمد عبيد بدوي
                            <br />
                            سما عبد العظيم ادهم مبروك
                            <br />
                            محمد احمد زكى عطيه
                            <br />
                            محمد ايمن عبد الظاهر محمد
                            <br />
                            محمود احمد ناصر محمود معوض
                            <br />
                            مصطفى محمود عماره عبد الفتاح

                        </p>



                    </div>

                </div>

                <img src="../../../public/Images/background.jpg" className={'bg-img'} alt="" />
                {/*<img src="../../../public/Images/colleg-logo.jpg" className={'logo'} draggable={false} alt=""/>*/}
                {/*<img src="../../../public/Images/logo.jpeg" className={'site-logo'} draggable={false} alt=""/>*/}
                <div className="bg"></div>


            </div>
        </section >
    );
};

export default Home;