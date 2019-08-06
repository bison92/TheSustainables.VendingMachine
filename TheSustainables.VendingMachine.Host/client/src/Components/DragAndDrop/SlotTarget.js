import React from 'react'
import { DropTarget } from 'react-dnd'
import ItemTypes from './ItemTypes'

const Target = ({ className, canDrop, isOver, connectDropTarget }) => {
    const isActive = canDrop && isOver
    let backgroundColor = '#989f77'
    if (isActive) {
        backgroundColor = 'darkgreen'
    } else if (canDrop) {
        backgroundColor = 'darkkhaki'
    }
    return (
        <div className={className} ref={connectDropTarget} style={{ backgroundColor }}>
            {isActive ? 'Release to insert' : 'Drop coins here'}
        </div>
    )
}
export default DropTarget(
    ItemTypes.COIN,
    {
        drop: () => ({ name: 'Slot' }),
    },
    (connect, monitor) => ({
        connectDropTarget: connect.dropTarget(),
        isOver: monitor.isOver(),
        canDrop: monitor.canDrop(),
    }),
)(Target)