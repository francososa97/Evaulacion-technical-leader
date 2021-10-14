import React,{useEffect,useState,useContext} from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import Button from "components/CustomButtons/Button.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Parallax from "components/Parallax/Parallax.js";
import styles from "assets/jss/material-kit-react/views/profilePage.js";
import {NavLink } from 'react-router-dom';
import {SubterraneoContext} from '../Context/SubterraneoContext';
import axios from 'axios';
import WarningNombreUsuario from "components/warningNombreUsuario/index";


const useStyles = makeStyles(styles);

const HistoricoIncidentes = () =>{

    const classes = useStyles();
    const[historialIncidentes,setHistorialIncidentes] = useState({})
    const[historialDisponible,setHistorialDisponible] = useState(false);
    const {nombreUsuario}  = useContext(SubterraneoContext);

    useEffect(() => {
      const getHistorialIncidentes = async () => {
        let endopoint = `https://localhost:44307/mock/alertaestado/${nombreUsuario}`
        axios.get(endopoint)
            .then(function (response) {
                setHistorialIncidentes(response.data);
                setHistorialDisponible(true);
            })
            .catch(function (error) {
                console.log(error);
            })
      }
      getHistorialIncidentes();
    }, []);

    return (
        <div>
        <Parallax
          small
          filter
          image={require("assets/img/background-subte3.jpg").default}
        />
        <div className={classNames(classes.main, classes.mainRaised)}>
          <div>
            <div className={classes.container}>
              <GridContainer justify="center">
                <GridItem xs={12} sm={12} md={6}>
                </GridItem>
              </GridContainer>
              <div className={classes.description}>
                  <h3>Historico Incidentes</h3>
                  <GridContainer justify="center">
                          <GridItem xs={6} sm={6} md={6}>
                          <p>Causa de demora {historialIncidentes.causaDemora}</p>
                        </GridItem>
                        <GridItem xs={6} sm={6} md={6}>
                        <p>El servicio se ve afectado con {historialIncidentes.efectoDemora}</p>
                        </GridItem>
                        <GridItem xs={12} sm={12} md={12}>
                          <p>{historialIncidentes.descripcionDemora}</p>
                        </GridItem>
                        <GridContainer justify="center">
                        {historialDisponible
                        ?historialIncidentes.periodoIncidentes.map(incidentes=>{
                            return(
                              <GridItem xs={12} sm={12} md={12}>
                                <p>Periodo de incidente</p>
                                <GridItem xs={12} sm={12} md={12}>
                                <p>{incidentes.inicioIncidente} a {incidentes.finIncidente}</p>
                                </GridItem>
                              </GridItem>
                            )
                         })
                        :null}
                        </GridContainer>
                  </GridContainer>
              </div>
              <GridContainer justify="center">
                <GridItem xs={12} sm={12} md={8} className={classes.navWrapper}>
                        <NavLink 
                            to='/menu'
                        >
                            <Button color="info" simple>
                                Volver
                            </Button>
                        </NavLink>
                        <WarningNombreUsuario/>
                </GridItem>
              </GridContainer>
            </div>
          </div>
        </div>
      </div>
    )
}
export default HistoricoIncidentes;