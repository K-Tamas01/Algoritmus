#include <stdio.h>
#include <omp.h>
#include <time.h>

int main(int argc, char** argv){
    float sum_part, total_sum;
    int size = 10000, count = 0, thread[] = {1,2,4,6,12};
    double s_time,e_time;
    do{
        count = 0;
        s_time = 0;
        e_time = 0;
        s_time = omp_get_wtime();
        do{
            srand(time(0));
            omp_set_dynamic(thread[count]);
            omp_set_num_threads(thread[count]);
            #pragma omp parallel
            {
                sum_part = 0;
                total_sum = 0;

             #pragma omp for
                    for(int i = 0;i<=size;i++){
                        sum_part += (rand() % 100000) + 1;
                    }
            }

            #pragma omp critical
            {
                    total_sum += sum_part;
            }

            e_time = omp_get_wtime();

            printf("szal: %d , elapsed time: %lf (sec) \n",omp_get_max_threads(), (e_time - s_time));
            printf("osszeg: %lf\n",total_sum);

            count++;
        }while(count < 5);
        size += 10000;
    }while(size <= 120000);

    return 0;
}