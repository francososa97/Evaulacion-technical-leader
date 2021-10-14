import React,{useContext,useState} from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
// @material-ui/icons
import People from "@material-ui/icons/People";
// core components
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Button from "components/CustomButtons/Button.js";
import Card from "components/Card/Card.js";
import CardBody from "components/Card/CardBody.js";
import CardHeader from "components/Card/CardHeader.js";
import CardFooter from "components/Card/CardFooter.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import {NavLink } from 'react-router-dom';
import styles from "assets/jss/material-kit-react/views/loginPage.js";
import {SubterraneoContext} from '../Context/SubterraneoContext';
import image from "assets/img/background-subte.jpg";

const useStyles = makeStyles(styles);

const Onbording = () =>{

    const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
    setTimeout(function () {
      setCardAnimation("");
    }, 700);
    const classes = useStyles();

    const {nombreUsuario,setNombreUsuario}  = useContext(SubterraneoContext);
    console.log(nombreUsuario);
    console.log(setNombreUsuario);

    return (
        
      <div>
      <div
        className={classes.pageHeader}
        style={{
          backgroundImage: "url(" + image + ")",
          backgroundSize: "cover",
          backgroundPosition: "top center",
        }}
      >
        <div className={classes.container}>
          <GridContainer justify="center">
            <GridItem xs={12} sm={12} md={4}>
              <Card className={classes[cardAnimaton]}>
                <form className={classes.form}>
                  <CardHeader color="info" className={classes.cardHeader}>
                    <h4>Log in</h4>
                  </CardHeader>
                  <CardBody>
                    <CustomInput
                      labelText="Nombre de usuario..."
                      id="first"
                      value={nombreUsuario}
                      onChange={(e) => setNombreUsuario(e.target.value)}
                      formControlProps={{
                        fullWidth: true,
                      }}
                      inputProps={{
                        type: "text",
                        endAdornment: (
                          <InputAdornment position="end">
                            <People className={classes.inputIconsColor} />
                          </InputAdornment>
                        ),
                      }}
                    />
                  </CardBody>
                  <CardFooter className={classes.cardFooter}>
                  {nombreUsuario.length > 0?
                      <NavLink
                            to='/menu'
                            >
                          <Button simple color="info" size="lg">
                            Comenzar
                          </Button>
                      </NavLink>
                    :null}
                  </CardFooter>
                </form>
              </Card>
            </GridItem>
          </GridContainer>
        </div>
      </div>
    </div>
    )
}
export default Onbording;