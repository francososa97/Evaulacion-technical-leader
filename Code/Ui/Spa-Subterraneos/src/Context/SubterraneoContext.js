import React, { createContext, useState} from 'react';

export const SubterraneoContext = createContext();

const SubterraneoProvider = (props) => {

    const [nombreUsuario, setNombreUsuario] = useState("");

    let values = {nombreUsuario,setNombreUsuario}
    return (
                <SubterraneoContext.Provider
                value={values}
            >
                {props.children}
            </SubterraneoContext.Provider>
    )
}
export default SubterraneoProvider;