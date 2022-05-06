#include <stdio.h>
#include <pthread.h>
#include <time.h>

#define THREAD 5
#define N 1000000

void sum(int length);

int part_sum,total_sum;
clock_t start,end;
double total;

int main(){

    pthread_t thread_id[THREAD];

    for(int i = 1;i<THREAD;i++)
        {
            part_sum = 0,total_sum = 0;

            int border[1+i];
            start = clock();
            for(int j = 0;j<i;j++){
                border[j] = N/i;
                pthread_create( &thread_id[i], NULL, sum, border[j] );
                pthread_join( thread_id[i], NULL);
            }
            total_sum += part_sum;
            end = clock();
            total = (double)(end - start) / CLOCKS_PER_SEC;
            printf("szal: %d\n",i);
            printf("osszeg (10 000 elem): %d\n",total_sum);
            printf("futasi ido: %lf (sec)\n",total);
            
        }

    return 0;
}

void sum(int length){
    srand(time(0));
    for(int i = 0;i<N;i++)
    {
        part_sum += (rand() % N) + 1;
    }
}