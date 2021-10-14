import React from "react";
import { createBrowserHistory } from "history";
import { Router, Route, Switch } from "react-router-dom";

//default styles
import "./assets/scss/material-kit-react.scss?v=1.10.0";

// paginas de la spa
import HistoricoIncidentes from "./views/HistoricoIncidentes.js";
import Menu from "./views/Menu.js";
import Onbording from "./views/Onbording.js";
import ProximoSubterraneo from "./views/ProximoSubterraneo.js";
import RegistroHistoricoIncidentes from "./views/RegistroHistoricoIncidentes.js";
import ServiciosRamales from "./views/ServiciosRamales.js";
import SubterraneoContext from './Context/SubterraneoContext';


const GestionSubterraneo = () =>{

    var hist = createBrowserHistory();

    return(
        <Router history={hist}>
            <SubterraneoContext>
                <Switch>
                    <Route exact path="/"  component={Onbording} />
                    <Route exact path="/menu" component={Menu} />
                    <Route exact path="/historico-incidentes" component={HistoricoIncidentes} />
                    <Route exact path="/proximo-subterraneo" component={ProximoSubterraneo}/>
                    <Route exact path="/registro-historico-incidentes" component={RegistroHistoricoIncidentes} />
                    <Route exact path="/servicio-ramal" component={ServiciosRamales} />
                </Switch>
            </SubterraneoContext>
        </Router>
        
    )
}
export default GestionSubterraneo;