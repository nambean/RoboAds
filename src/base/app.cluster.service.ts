import * as os from 'os';
import { Injectable } from '@nestjs/common';

const numCPUs = os.cpus().length;
const cluster = require('cluster');

@Injectable()
export class AppClusterService {
    static register(callback: Function): void {
        if (cluster.isMaster) {
            console.log(`Master server started on ${process.pid}`);
            for (let i = 0; i < numCPUs; i++) {
                cluster.fork();
            }

            cluster.on('exit', (worker, code, signal) => {
                console.log(`Worker ${worker.process.pid} died. Restarting`);
                cluster.fork();
            });
        } else {
            console.log(`Cluster server started on ${process.pid}`);
            callback();
        }
    }
}
