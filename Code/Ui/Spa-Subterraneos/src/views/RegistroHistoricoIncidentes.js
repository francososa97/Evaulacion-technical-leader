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
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import axios from 'axios';

const useStyles = makeStyles(styles);

const RegistroHistoricoIncidentes = () =>{

    const classes = useStyles();
    const[registros,setRegistros] = useState([]);

    useEffect(() => {
      const getHistorialIncidentes = async () => {

        let endopoint = `https://localhost:44307/alertas`
        axios.get(endopoint)
            .then(function (response) {
              setRegistros(response.data);
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
                  <h3>Historial de incidentes</h3>
              </div>
              <GridItem xs={12} sm={12} md={12} className={classes.navWrapper}>
              <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                  <TableHead>
                    <TableRow>
                      <TableCell>Id</TableCell>
                      <TableCell>Fecha incio Incidente</TableCell>
                      <TableCell>Fecha fin Incidente</TableCell>
                      <TableCell>Fecha Registrado</TableCell>
                      <TableCell>Nombre de usuario</TableCell>
                      <TableCell>Causa del incidente</TableCell>
                      <TableCell>Efecto en la linea</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {registros.map(registro=>{
                          return(
                            <TableRow
                            key={registro.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                          >
                            <TableCell>{registro.id}</TableCell>
                            <TableCell>{registro.fechaInicio}</TableCell>
                            <TableCell>{registro.fechaFin}</TableCell>
                            <TableCell>{registro.fechaConsultado}</TableCell>
                            <TableCell>{registro.nombreUsuario}</TableCell>
                            <TableCell>{registro.causa.descripcion == null ? null : registro.causa.descripcion}</TableCell>
                            <TableCell>{registro.efecto.descripcion == null ? null : registro.efecto.descripcion}</TableCell>
                          </TableRow>
                          )
                      })}
                  </TableBody>
                </Table>
              </TableContainer>
              </GridItem>
              <GridContainer justify="center">
                <GridItem xs={12} sm={12} md={8} className={classes.navWrapper}>
                        <NavLink
                            to='/menu'
                        >
                            <Button color="info" simple>
                                Volver
                            </Button>
                        </NavLink>
                </GridItem>
              </GridContainer>
            </div>
          </div>
        </div>
      </div>
    )
}
export default RegistroHistoricoIncidentes;


/*
                    {rows.map((row) => (
                      <TableRow
                        key={row.name}
                        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                      >
                        <TableCell component="th" scope="row">
                          {row.name}
                        </TableCell>
                        <TableCell align="right">{row.calories}</TableCell>
                        <TableCell align="right">{row.fat}</TableCell>
                        <TableCell align="right">{row.carbs}</TableCell>
                        <TableCell align="right">{row.protein}</TableCell>
                      </TableRow>
                    ))}

*/