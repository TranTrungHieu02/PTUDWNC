import { Link } from "react-router-dom";
import { useQuery } from '../Utils/Utils'

const BadRequest = () => {
    
    let query = useQuery(), redirectTo = query.get('redirectTo') ?? '/';

    return(
        <>
        <div>
            <h1>400</h1>
            <h1>Yêu cầu không hợp lệ</h1>
        </div>
        </>
    );
}

export default BadRequest;