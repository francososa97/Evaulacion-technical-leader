import React,{useEffect,useState,useContext} from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import Button from "components/CustomButtons/Button.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Parallax from "components/Parallax/Parallax.js";
import NavPills from "components/NavPills/NavPills.js";
import styles from "assets/jss/material-kit-react/views/profilePage.js";
import {NavLink } from 'react-router-dom';
import axios from 'axios';
import errorIcon from "../assets/img/sign_icon.png";
import checkIcon from "../assets/img/check_icon.png";
import WarningNombreUsuario from "components/warningNombreUsuario/index";
import {SubterraneoContext} from '../Context/SubterraneoContext';


const useStyles = makeStyles(styles);

const ServiciosRamales = () =>{

    const classes = useStyles();
    const[servicioRamales,setServiciosRamales] = useState({});
    const[servicioRamalesDisponible,setServicioRamalesDisponible] = useState(false);
    const {nombreUsuario}  = useContext(SubterraneoContext);

    useEffect(() => {
      const getServiciosRamalaes = async () => {
        let endopoint = `https://localhost:44307/alertaestado/${nombreUsuario}`
        axios.get(endopoint)
            .then(function (response) {
                setServiciosRamales(response.data);
                setServicioRamalesDisponible(true);
            })
            .catch(function (error) {
                console.log(error);
                setServiciosRamales(null);
            })
            .then(function () {
            });
         
      }
      getServiciosRamalaes();
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
                  <h3>Servicios Ramal</h3>
              <NavPills
                color="info"
                horizontal={{
                  tabsGrid: { xs: 12, sm: 4, md: 4 },
                  contentGrid: { xs: 12, sm: 8, md: 8 },
                }}
                tabs={[
                  {
                    tabButton: "A",
                    tabContent: (
                      <span>
                        {servicioRamalesDisponible
                        ?
                        servicioRamales.ramalAfectado == "A"
                          ?
                            <GridContainer justify="center">
                              <GridItem xs={12} sm={12} md={12}>
                                <img className="icon-exito" src={errorIcon}/>
                              </GridItem>
                              <GridItem xs={6} sm={6} md={6}>
                              <p>Causa de demora {servicioRamales.causaDemora}</p>
                            </GridItem>
                            <GridItem xs={6} sm={6} md={6}>
                            <p>El servicio se ve afectado con {servicioRamales.efectoDemora}</p>
                            </GridItem>
                            <GridItem xs={12} sm={12} md={12}>
                              <p>{servicioRamales.descripcionDemora}</p>
                            </GridItem>
                            {servicioRamales.periodoIncidentes.map(periodo=>{
                              return(
                                <GridItem xs={12} sm={12} md={12}>
                                  <p>Periodo Incidente</p>
                                  <p>{periodo.inicioIncidente} a {periodo.finIncidente}</p>
                                </GridItem>
                              )
                            })}
                            </GridContainer>
                          :
                            <GridContainer justify="center">
                              <GridItem xs={12} sm={12} md={12}>
                                <img className="icon-exito" src={checkIcon}/>
                              </GridItem>
                            <GridItem xs={12} sm={12} md={12}>
                              <p>Servicio funcionando con Normalidad</p>
                            </GridItem>
                          </GridContainer>
                        :null}
                    </span>
                    ),
                  },
                  {
                    tabButton: "B",
                    tabContent: (
                      <span>
                        {servicioRamalesDisponible
                        ?
                          servicioRamales.ramalAfectado == "B"
                          ?
                          <GridContainer justify="center">
                            <GridItem xs={12} sm={12} md={12}>
                              <img className="icon-exito" src={errorIcon}/>
                            </GridItem>
                            <GridItem xs={6} sm={6} md={6}>
                            <p>Causa de demora {servicioRamales.causaDemora}</p>
                          </GridItem>
                          <GridItem xs={6} sm={6} md={6}>
                          <p>El servicio se ve afectado con {servicioRamales.efectoDemora}</p>
                          </GridItem>
                          <GridItem xs={12} sm={12} md={12}>
                            <p>{servicioRamales.descripcionDemora}</p>
                          </GridItem>

                          { servicioRamales.length >0 ?
                          servicioRamales.periodoIncidentes.map(periodo=>{
                            return(
                              <GridItem xs={12} sm={12} md={12}>
                                <p>Periodo Incidente</p>
                                <p>{periodo.inicioIncidente} a {periodo.finIncidente}</p>
                              </GridItem>
                            )
                            })
                          : <p>No se encuentran periodos defindios para este Incidente</p>}
                          </GridContainer>
                          :<GridContainer justify="center">
                            <GridItem xs={12} sm={12} md={12}>
                              <img className="icon-exito" src={checkIcon}/>
                            </GridItem>
                          <GridItem xs={12} sm={12} md={12}>
                            <p>Servicio funcionando con Normalidad</p>
                          </GridItem>

                          </GridContainer>
                        :null}
                      </span>
                    ),
                  },
                  {
                    tabButton: "C",
                    tabContent: (
                      <span>
                        {servicioRamalesDisponible
                        ? 
                          servicioRamales.ramalAfectado == "C"
                          ?
                        <GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <img className="icon-exito" src={errorIcon}/>
                          </GridItem>
                          <GridItem xs={6} sm={6} md={6}>
                          <p>Causa de demora {servicioRamales.causaDemora}</p>
                        </GridItem>
                        <GridItem xs={6} sm={6} md={6}>
                        <p>El servicio se ve afectado con {servicioRamales.efectoDemora}</p>
                        </GridItem>
                        <GridItem xs={12} sm={12} md={12}>
                          <p>{servicioRamales.descripcionDemora}</p>
                        </GridItem>
                        {servicioRamales.periodoIncidentes.map(periodo=>{
                          return(
                            <GridItem xs={12} sm={12} md={12}>
                              <p>Periodo Incidente</p>
                              <p>{periodo.inicioIncidente} a {periodo.finIncidente}</p>
                            </GridItem>
                          )
                        })}
                        </GridContainer>
                        :<GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <img className="icon-exito" src={checkIcon}/>
                          </GridItem>
                        <GridItem xs={12} sm={12} md={12}>
                          <p>Servicio funcionando con Normalidad</p>
                        </GridItem>

                        </GridContainer>
                        : null}
                      </span>
                    ),
                  },
                  {
                    tabButton: "D",
                    tabContent: (
                      <span>
                        {servicioRamalesDisponible
                        ? 
                          servicioRamales.ramalAfectado == "D"
                          ?
                          <GridContainer justify="center">
                            <GridItem xs={12} sm={12} md={12}>
                              <img className="icon-exito" src={errorIcon}/>
                            </GridItem>
                            <GridItem xs={6} sm={6} md={6}>
                            <p>Causa de demora {servicioRamales.causaDemora}</p>
                          </GridItem>
                          <GridItem xs={6} sm={6} md={6}>
                          <p>El servicio se ve afectado con {servicioRamales.efectoDemora}</p>
                          </GridItem>
                          <GridItem xs={12} sm={12} md={12}>
                            <p>{servicioRamales.descripcionDemora}</p>
                          </GridItem>
                          {servicioRamales.periodoIncidentes.map(periodo=>{
                            return(
                              <GridItem xs={12} sm={12} md={12}>
                                <p>Periodo Incidente</p>
                                <p>{periodo.inicioIncidente} a {periodo.finIncidente}</p>
                              </GridItem>
                            )
                          })}
                          </GridContainer>
                          :<GridContainer justify="center">
                            <GridItem xs={12} sm={12} md={12}>
                              <img className="icon-exito" src={checkIcon}/>
                            </GridItem>
                          <GridItem xs={12} sm={12} md={12}>
                            <p>Servicio funcionando con Normalidad</p>
                          </GridItem>

                          </GridContainer>
                        :null}
                      </span>
                    ),
                  },
                  {
                    tabButton: "E",
                    tabContent: (
                      <span>
                        {servicioRamalesDisponible
                        ? 
                        servicioRamales.ramalAfectado == "E"
                        ?
                        <GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <img className="icon-exito" src={errorIcon}/>
                          </GridItem>
                          <GridItem xs={12} sm={12} md={12}>
                          <p>{servicioRamales.descripcionIncidentes}</p>
                        </GridItem>
                          <GridItem xs={6} sm={6} md={6}>
                          <p>Causa de demora {servicioRamales.causaDemora}</p>
                        </GridItem>
                        <GridItem xs={6} sm={6} md={6}>
                        <p>El servicio se ve afectado con {servicioRamales.efectoIncidentes}</p>
                        </GridItem>
  

                        { servicioRamales.length >0 ?
                        servicioRamales.periodoIncidentes.map(periodo=>{
                          return(
                            <GridItem xs={12} sm={12} md={12}>
                              <p>Periodo Incidente</p>
                              <p>{periodo.inicioIncidente} a {periodo.finIncidente}</p>
                            </GridItem>
                          )
                          })
                        : <p>No se encuentran periodos defindios para este Incidente</p>}
                        </GridContainer>
                        :<GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <img className="icon-exito" src={checkIcon}/>
                          </GridItem>
                        <GridItem xs={12} sm={12} md={12}>
                          <p>Servicio funcionando con Normalidad</p>
                        </GridItem>

                        </GridContainer>
                        :null}
                      </span>
                    ),
                  },
                  {
                    tabButton: "H",
                    tabContent: (
                      <span>
                        {servicioRamalesDisponible
                        ? 
                          servicioRamales.ramalAfectado == "H"?
                        <GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <img className="icon-exito" src={errorIcon}/>
                          </GridItem>
                          <GridItem xs={6} sm={6} md={6}>
                          <p>Causa de demora {servicioRamales.causaDemora}</p>
                        </GridItem>
                        <GridItem xs={6} sm={6} md={6}>
                        <p>El servicio se ve afectado con {servicioRamales.efectoDemora}</p>
                        </GridItem>
                        <GridItem xs={12} sm={12} md={12}>
                          <p>{servicioRamales.descripcionDemora}</p>
                        </GridItem>
                        {servicioRamales.periodoIncidentes.map(periodo=>{
                          return(
                            <GridItem xs={12} sm={12} md={12}>
                              <p>Periodo Incidente</p>
                              <p>{periodo.inicioIncidente} a {periodo.finIncidente}</p>
                            </GridItem>
                          )
                        })}
                        </GridContainer>
                        :<GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <img className="icon-exito" src={checkIcon}/>
                          </GridItem>
                        <GridItem xs={12} sm={12} md={12}>
                          <p>Servicio funcionando con Normalidad</p>
                        </GridItem>
                        </GridContainer>
                        :null}
                      </span>
                    ),
                  },
                ]}
              />
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
export default ServiciosRamales;