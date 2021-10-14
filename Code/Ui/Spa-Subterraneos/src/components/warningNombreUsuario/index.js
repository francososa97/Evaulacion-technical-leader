import React,{useContext} from 'react';
import Alert from '@material-ui/lab/Alert';
import {SubterraneoContext} from '../../Context/SubterraneoContext';
import {NavLink } from 'react-router-dom';

export default function warningNombreUsuario() {
    const {nombreUsuario}  = useContext(SubterraneoContext);
  return (
      <>
      {nombreUsuario == "" || nombreUsuario == undefined || nombreUsuario == null 
      ?
        <Alert severity="warning">Advertencia! — tu nombre de usuario a sido eliminado de memoria, por  favor ingrese <NavLink to='/'>aqui</NavLink> para volver a registrar! sin esta informacion podria no obtener la informacion del backend</Alert>
      :null}
    
    </>
  );
}