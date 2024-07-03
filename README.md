# 1. Introduction

## Présentation du Projet
OptiFrame est un plugin pour Unity qui permet de populariser leurs nouvelles technologies DOTS (Data-Oriented Technology Stack) afin d'optimiser les jeux, simulations et animations. Ce projet s'accompagne d'un jeu de type RTS (jeu de stratégie en temps réel) afin de comprendre, tester et saisir les points clé de cette nouvelle technologie. Ce projet permettra d'optimiser le développement avec DOTS, le temps de rendu, l'usure des machines, ou encore la consommation d'énergie demandée.

L'idée est née de la volonté de créer un jeu de type RTS, simulation en temps réel, à une très grande échelle, pour pousser le réalisme au maximum. Nous avions entendu parler de la technologie DOTS, qui nous semble parfaite pour ce genre de projet, et nous souhaitons pouvoir la rendre disponible au plus grand nombre.
# 2. Vue d'ensemble de DOTS
## Qu'est-ce que DOTS ?
DOTS (Data-Oriented Technology Stack) est un ensemble d'outils mis à disposition par Unity, ayant pour objectif d'améliorer la performance des jeux en optimisant la manière dont les données sont utilisées.
DOTS est composé de 3 éléments:
### ECS
L'ECS de Unity (Entity Component System) vous permet de créer des jeux plus ambitieux. C'est un framework orienté données compatible avec les GameObjects, permettant aux créateurs expérimentés de Unity d'accomplir davantage grâce à un niveau de contrôle et de déterminisme très élevé.
### Compilateur Burst
Burst est un compilateur que vous pouvez utiliser avec le Job System d'Unity pour créer du code qui améliore les performances de votre application. Il traduit votre code du bytecode IL/.NET en code CPU natif optimisé en utilisant le compilateur LLVM.
### Job System
Le Job System vous permet d'écrire du code multithreadé simple et sécurisé afin que votre application puisse utiliser tous les cœurs de CPU disponibles pour exécuter votre code. Cela permet d'aider à améliorer les performances de votre application.
# 3. Présentation de OptiFrame
## Fonctionnalités prévues
Le projet aura la forme d'un plugin Unity, qui permettera à l'utilisateur d'avoir les fichiers de base pour pouvoir utiliser la technologie DOTS directement, et également d'assister le développeur tout au long de la conception de son jeu. L'objectif est de rendre l'utilisation de DOTS la plus simple et efficace possible pour n'importe quelle personne souhaitant développer un jeu optimisé.

Nous développerons en parallèle un jeu qui nous permettra de mieux comprendre la technologie DOTS et de saisir les points essentiels de la technologie. De plus, ces connaissances nous permettrons de créer plus efficacement le plugin, rendant ainsi plus agréable le développement avec la technologie.
## Intégration avec Unity
Le projet sera un plugin Unity, disponible sur l'AssetStore de Unity.
# 4. Plan de Développement
## Phases de Développement
![Roadmap d'OptiFrame](https://i.imgur.com/NbEf8oQ.jpeg)
# 5. Utilisation prévue de OptiFrame
## Scénarios d'utilisation
 Notre projet sera utile aux personnes souhaitant développer des jeux de type RTS, ou avec une utilisation du processeur très élevée. Ce projet s'adresse également aux personnes faisant de l'animation sur Unity, qui pourront alors faire des rendus beaucoup plus rapidement et avec un matériel plus abordable.
# 6. Projet de jeu RTS
## Explication du jeu
 Le projet de jeu RTS (jeu de stratégie en temps réel) nous permettrait d'en apprendre le plus possible sur la technologie DOTS, car ce genre de jeu est très gourmand en ressources, et ainsi pouvoir proposer une solution le mieux adapté à ce genre de projets. 
# 7. Conclusion
## Résumé des Bénéfices Attendus
L'objectif de ce projet est de pouvoir créer et mettre à disposition des jeux avec des performances améliorées sans avoir besoin d'investir dans du matériel de très haute qualité, ainsi que réduire les coûts énergétiques nécessaires pour jouer, et parallèlement allonger la durée de vie des composants utilisés, car ils seront mieux exploités.

Une évolution possible de ce projet pourrait être un convertisseur, permettant de convertir un jeu déja créé qui n'utilise actuellement pas la technologie DOTS, en un jeu jouable immédiatement, et qui verrait ses performances grandement améliorées via DOTS. 
