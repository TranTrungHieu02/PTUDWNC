import React, {useEffect} from "react";

const Rss = () => {
    useEffect(() => {
        document.title = 'Liên hệ';
    }, []);

    return (
        <h1>
            Đây là trang để liên hệ
        </h1>
    );
}

export default Rss;