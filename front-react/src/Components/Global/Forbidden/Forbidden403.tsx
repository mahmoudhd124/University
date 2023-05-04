import React, {useEffect, useRef, useState} from 'react';
import './Forbidden403.css'
import ForbiddenTypeWriter from "./ForbiddenTypeWriter";

const Forbidden403 = () => {
    return (
        <div className="forbidden mt-5">
            <div className={'forbidden-container'}>
                <h1 className={'forbidden-h1'}>403</h1>
                <div className={'container forbidden-div'}>
                    <ForbiddenTypeWriter title={"ERROR CODE"}
                                         text={"HTTP 403 Forbidden"}
                                         time={2}
                    />

                    <ForbiddenTypeWriter title={"ERROR DESCRIPTION"}
                                         text={"Access Denied. You Do Not Have The Permission To Access This Page On This Server"}
                                         time={2}
                    />

                    <ForbiddenTypeWriter title={"ERROR POSSIBLY CAUSED BY"}
                                         text={"[execute access forbidden, read access forbidden, write access forbidden, ssl required, ssl 128 required, ip address rejected, client certificate required, site access denied, too many users, invalid configuration, password change, mapper denied access, client certificate revoked, directory listing denied, client access licenses exceeded, client certificate is untrusted or invalid, client certificate has expired or is not yet valid, passport logon failed, source access denied, infinite depth is denied, too many requests from the same client ip...]"}
                                         time={2}
                    />

                    <ForbiddenTypeWriter title={"SOME PAGES ON THIS SERVER THAT YOU DO HAVE PERMISSION TO ACCESS"}
                                         text={"[Home Page, Doctor...]"}
                                         time={2}
                    />

                    <ForbiddenTypeWriter title={"HAVE A NICE DAY SIR "}
                                         text={"-)"}
                                         time={2}
                    />
                </div>
            </div>
        </div>
    );

};

export default Forbidden403;