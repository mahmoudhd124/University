import React from 'react';

const DoctorField = ({title, data}: { title: string, data: string }) => {
    return (
        <div className='row justify-content-between justify-content-sm-center'>
            <span className="col-12 col-sm-3">{title}</span>
            <span className="fw-bold col-12 col-sm-5">{data}</span>
        </div>
    );
};

export default DoctorField;