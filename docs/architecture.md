# Архитектура (черновик)

```mermaid
stateDiagram-v2
    [*] --> Patrol
    Patrol --> Alert: see/hear player
    Alert --> Chase: confirmed
    Alert --> Patrol: timeout
    Chase --> Attack: in range
    Attack --> Chase: out of range
    Chase --> Patrol: lost target
```
