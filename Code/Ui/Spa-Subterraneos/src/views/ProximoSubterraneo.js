import React,{useContext,useState} from "react";
import axios from 'axios';
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
import Select from "@material-ui/core/Select";
import MenuItem from '@material-ui/core/MenuItem';
import {SubterraneoContext} from '../Context/SubterraneoContext';
import CardProximoTren from '../components/CardProximoTren/index';
import WarningNombreUsuario from "components/warningNombreUsuario/index";

const useStyles = makeStyles(styles);

const ProximoSubterraneo = () =>{

    const classes = useStyles();
    const {nombreUsuario}  = useContext(SubterraneoContext);
    const [ramalSeleccionado,setRamalSeleccionado] = useState("");
    const [estaciones,setEstaciones] = useState([]);
    const [estacionesDisponibles,setEstacionesDisponibles] = useState(false);
    const [estacionesOrigenSeleccionada,setEstacionesOrigenSeleccionada] = useState("");
    const [estacionesDestinoSelccionada,setEstacionesDestinoSelccionada] = useState("");
    const [proximoSubte,setProximoSubte] = useState({});
    const [proximoSubteDisponible,setProximoSubteDisponible] = useState(false);

    const getEstaciones = (ramalSeleccionado) =>{
      let endopoint = `https://localhost:44307/forecast/estacion/linea=${ramalSeleccionado}&usuario=${nombreUsuario}`
      axios.get(endopoint)
          .then(function (response) {
              setEstaciones(response.data)
              let disponibleMostrar = response.data.length > 0;
              setEstacionesDisponibles(disponibleMostrar);
          })
          .catch(function (error) {
              console.log(error);
              setEstaciones(["no encontrado"]);
          })
    }

    const BuscarProximoSubterraneo = () => {
      let endopoint = `https://localhost:44307/forecast/usuario=${nombreUsuario}&linea=${ramalSeleccionado}&estacion=${estacionesOrigenSeleccionada}&destino=${estacionesDestinoSelccionada}`
      axios.get(endopoint)
          .then(function (response) {
              console.log(response.data);
              setProximoSubte(response.data);
              setProximoSubteDisponible(true);
          })
          .catch(function (error) {
              console.log(error);
              setEstaciones(["no encontrado"]);
          })
    }
    const handleChange = (event) => {
      setRamalSeleccionado(event.target.value);
      getEstaciones(event.target.value);
    };

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
                  <h3>Proximo Subterraneo</h3>
              </div>
              <GridContainer justify="start">
              <GridItem xs={12} sm={12} md={6}>
                    <h4>Seleccione Ramal</h4>
                    <Select
                      labelId="demo-simple-select-standard-label"
                      id="demo-simple-select-standard"
                      value={ramalSeleccionado}
                      onChange={handleChange}
                      label="ramal Seleccionado"
                    >
                      <MenuItem value={"A"}>A</MenuItem>
                      <MenuItem value={"B"}>B</MenuItem>
                      <MenuItem value={"C"}>C</MenuItem>
                      <MenuItem value={"D"}>D</MenuItem>
                      <MenuItem value={"E"}>E</MenuItem>
                      <MenuItem value={"H"}>H</MenuItem>
                    </Select>
                </GridItem>
                {estacionesDisponibles 
                ?
                  <>
                      <GridItem xs={6} sm={6} md={6}>
                      <h4>Estacion Origen</h4>
                      <Select
                        labelId="demo-simple-select-standard-label"
                        id="demo-simple-select-standard"
                        value={estacionesOrigenSeleccionada}
                        onChange={(e) => setEstacionesOrigenSeleccionada(e.target.value)}
                        label="Estacion Disponible"
                      >
                        {estaciones.map(estacion=>{
                          return(
                            <MenuItem value={estacion}>{estacion}</MenuItem>
                          )
                        })}
                      </Select>
                  </GridItem>
                  <GridItem xs={6} sm={6} md={6}>
                      <h4>Estacion Destino</h4>
                      <Select
                        labelId="demo-simple-select-standard-label"
                        id="demo-simple-select-standard"
                        value={estacionesDestinoSelccionada}
                        onChange={(e) => setEstacionesDestinoSelccionada(e.target.value)}
                        label="Estacion Destino"
                      >
                        {estaciones.map(estacion=>{
                          return(
                            <MenuItem value={estacion}>{estacion}</MenuItem>
                          )
                        })}
                      </Select>
                  </GridItem>
                  </>
                : null}
                {proximoSubteDisponible
                ?                  
                  <GridItem xs={12} sm={12} md={12}>
                    <CardProximoTren proximoSubte={proximoSubte}/>
                  </GridItem>
                : null}
              </GridContainer>
              <GridContainer justify="center">
                <GridItem xs={12} sm={12} md={8} className={classes.navWrapper}>
                      <GridItem xs={12} sm={12} md={6}>
                          <Button color="info" onClick={() => BuscarProximoSubterraneo()}>
                              Buscar
                          </Button>
                      </GridItem>
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
export default ProximoSubterraneo;