import * as React from 'react';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

const CardProximoTren = (props) => {

    const {
        tiempoArriboTren,
        retrasoArrivo,
        origen,
        destino,
        mensajeViaje,
        destinoRamal,
        totalEstacionesRecorrido
    } = props.proximoSubte;
  return (
    <Card className="card-proximo-tren" sx={{ minWidth: 275 }}>
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          El proximo subterraneo llgara en <code>{tiempoArriboTren}</code> minutos a su estacion de origen
        </Typography>
        <Typography variant="h5" component="div">
        </Typography>
        <Typography sx={{ mb: 1.5 }} color="text.secondary">
          Su recorrido tiene un total de {totalEstacionesRecorrido} estaciones recorridas.
        </Typography>
        <Typography variant="body2">
            <h4>{origen}</h4> a <h4>{destino}</h4>
        </Typography>
        <Typography variant="body2">
            {mensajeViaje}
        </Typography>
      </CardContent>
    </Card>
  );
}
export default CardProximoTren;
