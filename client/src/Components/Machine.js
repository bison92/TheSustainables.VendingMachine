import React, { useState, useEffect } from 'react'
import { Container, Row, Col } from 'reactstrap';
import axios from 'axios'
import './Machine.css'
import Product from '../Components/Product/Product'

const Machine = () => {

    const [products, setProducts] = useState([]);

    useEffect(() => {
        axios.get('api/machine/products')
            .then((response) => setProducts(response.data.products));
    }, []);

    return (
        <Container className="machine">
            <Row>
                <Col md={8} className="showcase">
                    {products.map((product) => <Product name={product.name} price={product.price} />)}
                </Col>
                <Col md={4} className="panel">
                    <div className="lcdText"> </div>
                    <div className="inserCoint"> </div>
                </Col>
            </Row>
        </Container>
    )
}

export default Machine;