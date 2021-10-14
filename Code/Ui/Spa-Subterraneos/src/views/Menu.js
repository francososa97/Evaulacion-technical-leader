import React from "react";
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

const useStyles = makeStyles(styles);

const Menu = () =>{

    const classes = useStyles();
    return (
      <div>
      <Parallax small filter image={require("assets/img/background-subte3.jpg").default}/>
      <div className={classNames(classes.main, classes.mainRaised)}>
        <div>
          <div className={classes.container}>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={6}>
              </GridItem>
            </GridContainer>
            <div className={classes.description}>
                <h3>Opciones</h3>
                <NavLink 
                    to='/servicio-ramal'
                >
                    <Button color="info">
                        Obtener Servicios Actuales
                    </Button>
                </NavLink>

                <NavLink 
                    to='/proximo-subterraneo'
                >
                    <Button color="info">
                        Verificar poriximo subterraneo
                    </Button>
                </NavLink>

                <NavLink 
                        to='/historico-incidentes'
                    >
                    <Button color="info">
                        Historial de incidentes
                    </Button>
                </NavLink>
                <NavLink 
                        to='/registro-historico-incidentes'
                >
                    <Button color="info">
                        Solo administradores...    Registro de consultas de incidentes
                    </Button>
                </NavLink>
            </div>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={8} className={classes.navWrapper}>
              </GridItem>
            </GridContainer>
          </div>
        </div>
      </div>
    </div>
    )

}
export default Menu;