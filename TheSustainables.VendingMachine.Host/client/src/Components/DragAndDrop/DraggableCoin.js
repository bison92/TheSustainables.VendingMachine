import React from 'react'
import ItemTypes from './ItemTypes'
import Coin from '../Coin/Coin'
import axios from 'axios'
import { DragSource } from 'react-dnd'

const DraggableCoin = ({ updateCredit, value, isDragging, connectDragSource }) => {
    const opacity = isDragging ? 0.4 : 1
    return (
        <div ref={connectDragSource} style={{ float: "left", opacity }}>
            <Coin value={value} updateCredit={updateCredit} />
        </div>
    )
}

export default DragSource(
    ItemTypes.COIN,
    {
        beginDrag: props => ({ value: props.value, updateCredit: props.updateCredit }),
        endDrag(props, monitor) {
            const item = monitor.getItem()
            const dropResult = monitor.getDropResult()
            if (dropResult) {
                axios.put('api/machine/userCashTray', {value: item.value})
                    .then((result) => props.updateCredit(result.data));
            }
        },
    },
    (connect, monitor) => ({
        connectDragSource: connect.dragSource(),
        isDragging: monitor.isDragging(),
    }),
)(DraggableCoin)
