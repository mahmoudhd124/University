import UseAxiosApi from "../../Hookes/useAxiosApi";
import {useState} from "react";
import useAppSelector from "../../Hookes/useAppSelector";
import {useNavigate} from "react-router-dom";

const Profile = () => {
    const api = UseAxiosApi()
    const navigator = useNavigate()
    const isDoctor = useAppSelector(s => s.auth.roles)?.some(r => r.toLowerCase() == 'doctor')
    isDoctor && navigator('/doctor/') 
    
    const [d, setD] = useState<any>()
    ;(async () => {
        const r = await api.get('doctor/0/10')
        setD(r.data)
    })()
    return (
        <section>
            <h4>{JSON.stringify(d, null, '\n')}</h4>
        </section>
    );
};

export default Profile;